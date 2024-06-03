using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public CertificatesController(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCertificate(IFormFile file, string userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Validate file type and size
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var maxFileSize = 10 * 1024 * 1024; // 10MB

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                return BadRequest("Invalid file type. Only JPG, JPEG, PNG, and GIF files are allowed.");
            }

            if (file.Length > maxFileSize)
            {
                return BadRequest("File size exceeds the limit of 10MB.");
            }

            // Generate a unique file name to prevent overwriting existing files
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Get the directory path to store the file
            var uploadsDirectory = Path.Combine(_environment.WebRootPath, "uploads");

            // Ensure the uploads directory exists, if not, create it
            Directory.CreateDirectory(uploadsDirectory);

            var filePath = Path.Combine(uploadsDirectory, fileName);

            try
            {
                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Save file information to the database
                var certificate = new Certificate { Name = fileName, FilePath = filePath, AppUserId = userId };
                _context.Certificates.Add(certificate);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Certificate uploaded successfully", certificateId = certificate.Id });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex); // You can replace this with your preferred logging mechanism

                // Return a more informative error message
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save certificate information to the database.");
            }
        }


        
        [HttpGet("{userId}/photo")]
        public IActionResult GetCertificatePhotoByUserId(string userId)
        {
            
            var certificate = _context.Certificates.FirstOrDefault(c => c.AppUserId == userId);

            if (certificate == null)
            {
                return NotFound("There is no Certificate for you Right now");
            }

            var fileBytes = System.IO.File.ReadAllBytes(certificate.FilePath);
            return File(fileBytes, "image/jpeg"); 
        }
    }
}
