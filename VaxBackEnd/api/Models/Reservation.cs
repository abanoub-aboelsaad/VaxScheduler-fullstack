using System;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enum.cs;

namespace api.Models
{
    public class Reservation
    {
        public int Id { get; set; } // Primary Key

        public string DoseNumber { get; set; } // Enum: FirstDose, SecondDose

        public DateTime ReservationDate { get; set; }

        public string Status { get; set; } // Pending, Approved, Rejected

        public string AppUserId { get; set; }
        
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

      
        public int VaccineId { get; set; } 
      
        public Vaccine? Vaccine { get; set; }
        public int VaccinationCenterId { get; set; } 
        public VaccinationCenter? VaccinationCenter { get; set; } 
    }
}
