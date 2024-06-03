using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.App.Account.Dtos;
using api.App.Account.Interfaces;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEncryptionService _encryptionService;

        public AccountController(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEncryptionService encryptionService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _roleManager = roleManager;
            _encryptionService = encryptionService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.Username.ToLower());

            if (user == null)
                return Unauthorized("Invalid username!");

            var roles = user.Role;
            var isAdminOrVaccinationCenter = roles.Contains("Admin") || roles.Contains("VaccinationCenter");

            if (isAdminOrVaccinationCenter)
            {
                if (user.Status != "Accepted")
                {
                    user.Status = "Accepted";
                    await _userManager.UpdateAsync(user);
                }

               var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Username not found and/or password incorrect");
                return Ok(
                    new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = await _tokenService.CreateToken(user)
                    }
                );
            }
            else
            {
                if (user.Status == "Rejected")
                    return Unauthorized("Your registration has been rejected. Please call me.");

                // Check if the user's status is "Accepted" for other roles
                if (user.Status == "Pending")
                    return Unauthorized("Your registration is pending approval");

                // Allow login if status is "Accepted" for other roles
                var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Username not found and/or password incorrect");

                return Ok(
                    new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = await _tokenService.CreateToken(user)
                    }
                );
            }
        }

 [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
{
    try
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (registerDto.Role != "User" && registerDto.Role != "Admin" && registerDto.Role != "VaccinationCenter")
            return BadRequest("Invalid role. Role must be either User, Admin, or VaccinationCenter.");

        
        // string encryptionKey = _encryptionService.GenerateKey();
        
        var encryptedEmail = _encryptionService.Encrypt(registerDto.Email, "H+hoSmOnifKHFvntXnJ3bKFdxe9i4dSgSqPkaTA825U=");

        var appUser = new AppUser
        {
            UserName = registerDto.Username,
            Email = encryptedEmail,
            Status = "Pending",
            Role = registerDto.Role
        };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(appUser, registerDto.Role);
            if (roleResult.Succeeded)
            {
                return Ok(
                    new NewUserDto
                    {
                        UserName = registerDto.Username,
                        Email = registerDto.Email,
                        Token = await _tokenService.CreateToken(appUser)
                    }
                );
            }
            else
            {
                return StatusCode(500, roleResult.Errors);
            }
        }
        else
        {
            return StatusCode(500, createdUser.Errors);
        }
    }
    catch (Exception e)
    {
        return StatusCode(500, e);
    }
}
    [HttpPost("approve-user")]
public async Task<IActionResult> ApproveUser(string userId)
{
    return await ChangeUserStatus(userId, "Accepted", "User approved successfully");
}

[HttpPost("reject-user")]
public async Task<IActionResult> RejectUser(string userId)
{
    return await ChangeUserStatus(userId, "Rejected", "User rejected successfully");
}

private async Task<IActionResult> ChangeUserStatus(string userId, string newStatus, string successMessage)
{
    // Find the user by their ID
    var user = await _userManager.FindByIdAsync(userId);

    if (user == null)
    {
        return NotFound("User not found");
    }

    // Update the user's status
    user.Status = newStatus;
    var result = await _userManager.UpdateAsync(user);

    if (result.Succeeded)
    {
        return Ok(successMessage);
    }
    else
    {
        return StatusCode(500, "Failed to change user status");
    }
}


[HttpGet("pending-users")]
public async Task<IActionResult> GetPendingUsers()
{
    try
    {
        // Retrieve users with status pending
        var pendingUsers = await _userManager.Users
            .Where(u => u.Status == "Pending")
            .ToListAsync();

        // Retrieve the encryption key (assuming it's a constant or stored securely)
        // string encryptionKey = "H+hoSmOnifKHFvntXnJ3bKFdxe9i4dSgSqPkaTA825U=";

        // Create a list to hold decrypted user data
        var decryptedUsers = new List<PendingUserDto>();

        // Decrypt email addresses using the same encryption key for all users
        foreach (var user in pendingUsers)
        {
            // Decrypt the email address using the same encryption key
            var decryptedEmail = _encryptionService.Decrypt(user.Email, "H+hoSmOnifKHFvntXnJ3bKFdxe9i4dSgSqPkaTA825U=");

            // Create a new instance of PendingUserDto with decrypted email
            var decryptedUser = new PendingUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = decryptedEmail
            };

            decryptedUsers.Add(decryptedUser);
        }

        return Ok(decryptedUsers);
    }
    catch (Exception e)
    {
        return StatusCode(500, e.Message);
    }
}


    } 
}













      
        //  [HttpPut("change-role")]
        // public async Task<IActionResult> ChangeUserRole(string userId, string newRole)
        // {
        //     // Find the user by their ID
        //     var user = await _userManager.FindByIdAsync(userId);

        //     if (user == null)
        //     {
        //         return NotFound("User not found");
        //     }

        //     // Check if the new role exists
        //     if (!await _roleManager.RoleExistsAsync(newRole))
        //     {
        //         return BadRequest("Invalid role");
        //     }

        //     // Remove the user from all existing roles
        //     var userRoles = await _userManager.GetRolesAsync(user);
        //     await _userManager.RemoveFromRolesAsync(user, userRoles);

        //     // Add the user to the new role
        //     var result = await _userManager.AddToRoleAsync(user, newRole);

        //     if (result.Succeeded)
        //     {
        //         return Ok("User role changed successfully");
        //     }
        //     else
        //     {
        //         return StatusCode(500, "Failed to change user role");
        //     }
        // }




































// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using api.Dtos.Account;
// using api.Interfaces;
// using api.Models;
// using api.Service;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace api.Controllers
// {
//     [Route("api/account")]
//     [ApiController]
//     public class AccountController : ControllerBase
//     {
//         private readonly UserManager<AppUser> _userManager;
//         private readonly ITokenService _tokenService;
//         private readonly SignInManager<AppUser> _signInManager;
//         private readonly RoleManager<AppUser> _roleManager;
//         private readonly IEncryptionService _encryptionService;

//         public AccountController(
//             UserManager<AppUser> userManager,
//             ITokenService tokenService,
//             SignInManager<AppUser> signInManager,
//             RoleManager<AppUser> roleManager,
//             IEncryptionService encryptionService)
//         {
//             _userManager = userManager;
//             _tokenService = tokenService;
//             _signInManager = signInManager;
//             _roleManager = roleManager;
//             _encryptionService = encryptionService;
//         }

//         [HttpPost("login")]
//         public async Task<IActionResult> Login(LoginDto loginDto)
//         {
//             if (!ModelState.IsValid)
//                 return BadRequest(ModelState);

//             var user = await _userManager.FindByNameAsync(loginDto.Username.ToLower());

//             if (user == null)
//                 return Unauthorized("Invalid username!");

//             var roles = user.Role;
//             var isAdminOrVaccinationCenter = roles.Contains("Admin") || roles.Contains("VaccinationCenter");

//             if (isAdminOrVaccinationCenter)
//             {
//                 if (user.Status != "Accepted")
//                     user.Status = "Accepted";
//                 await _userManager.UpdateAsync(user);

//                 // Allow login without checking status
//                 return Ok(
//                     new NewUserDto
//                     {
//                         UserName = user.UserName,
//                         Email = _encryptionService.Decrypt(user.Email),
//                         Token = await _tokenService.CreateToken(user)
//                     }
//                 );
//             }
//             else
//             {

//                 if (user.Status == "Rejected")
//                     return Unauthorized("Your registration has been rejected. Please call me.");

//                 // Check if the user's status is "Accepted" for other roles
//                 if (user.Status == "Pending")
//                     return Unauthorized("Your registration is pending approval");

//                 // Allow login if status is "Accepted" for other roles
//                 var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
//                 if (!result.Succeeded)
//                     return Unauthorized("Username not found and/or password incorrect");

//                 return Ok(
//                     new NewUserDto
//                     {
//                         UserName = user.UserName,
//                         Email = _encryptionService.Decrypt(user.Email),
//                         Token = await _tokenService.CreateToken(user)
//                     }
//                 );
//             }
//         }

//         [HttpPost("register")]
//         public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
//         {
//             try
//             {
//                 if (!ModelState.IsValid)
//                     return BadRequest(ModelState);

//                 if (registerDto.Role != "User" && registerDto.Role != "Admin" && registerDto.Role != "VaccinationCenter")
//                     return BadRequest("Invalid role. Role must be either User, Admin, or VaccinationCenter.");

//                 var appUser = new AppUser
//                 {
//                     UserName = registerDto.Username,
//                     Email = _encryptionService.Encrypt(registerDto.Email),
//                     Status = "Pending",
//                     Role = registerDto.Role
//                 };

//                 var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

//                 if (createdUser.Succeeded)
//                 {
//                     var roleResult = await _userManager.AddToRoleAsync(appUser, registerDto.Role);
//                     if (roleResult.Succeeded)
//                     {
//                         return Ok(
//                             new NewUserDto
//                             {
//                                 UserName = appUser.UserName,
//                                 Email = registerDto.Email,
//                                 Token = await _tokenService.CreateToken(appUser)
//                             }
//                         );
//                     }
//                     else
//                     {
//                         return StatusCode(500, roleResult.Errors);
//                     }
//                 }
//                 else
//                 {
//                     return StatusCode(500, createdUser.Errors);
//                 }
//             }
//             catch (Exception e)
//             {
//                 return StatusCode(500, e);
//             }
//         }

//         [HttpPost("approve-user")]
//         public async Task<IActionResult> ApproveUser(string userId)
//         {
//             return await ChangeUserStatus(userId, "Accepted", "User approved successfully");
//         }

//         [HttpPost("reject-user")]
//         public async Task<IActionResult> RejectUser(string userId)
//         {
//             return await ChangeUserStatus(userId, "Rejected", "User rejected successfully");
//         }

//         private async Task<IActionResult> ChangeUserStatus(string userId, string newStatus, string successMessage)
//         {
//             // Find the user by their ID
//             var user = await _userManager.FindByIdAsync(userId);

//             if (user == null)
//             {
//                 return NotFound("User not found");
//             }

//             // Update the user's status
//             user.Status = newStatus;
//             var result = await _userManager.UpdateAsync(user);

//             if (result.Succeeded)
//             {
//                 return Ok(successMessage);
//             }
//             else
//             {
//                 return StatusCode(500, "Failed to change user status");
//             }
//         }

//         [HttpGet("pending-users")]
//         public async Task<IActionResult> GetPendingUsers()
//         {
//             try
//             {
//                 // Retrieve users with status pending
//                 var pendingUsers = await _userManager.Users
//                     .Where(u => u.Status == "Pending")
//                     .Select(u => new { Id = u.Id, UserName = u.UserName, Email = u.Email })
//                     .ToListAsync();

//                 return Ok(pendingUsers);
//             }
//             catch (Exception e)
//             {
//                 return StatusCode(500, e.Message);
//             }
//         }
//     } 
// }
