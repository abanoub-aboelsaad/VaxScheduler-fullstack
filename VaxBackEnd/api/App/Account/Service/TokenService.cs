using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
   public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            // Get the user's roles
            var roles = await _userManager.GetRolesAsync(user);

            // Create a list of claims
            var claims = new List<Claim>
            {
               
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim("status", user.Status),
                new Claim("role", user.Role)
            };

            // Add user roles as claims
            // foreach (var role in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, role));
            // }

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            // Create token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Write token
            return tokenHandler.WriteToken(token);
        }
    }
}


