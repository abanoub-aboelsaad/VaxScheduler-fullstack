using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;


namespace api.Models
{
    public class AppUser : IdentityUser
    {
 
        //  [Required]

        public string Status { get; set; } = String.Empty;

        // [Required]

         public VaccinationCenter? VaccinationCenter { get; set; }

//   [Required]    
         public string Role { get; set; } = String.Empty;
         

        public List<Certificate> Certificates { get; set; } = new List<Certificate>();     
 public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}













