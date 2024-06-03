using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enum.cs;

namespace api.Dtos.Reservation
{
    public class ReservationDto
    {
        
        public required string DoseNumber { get; set; } // Enum: FirstDose, SecondDose

        public DateTime ReservationDate { get; set; }

        // public ReservationStatus Status { get; set; } // Pending, Approved, Rejected


        public required string AppUserId { get; set; }
      
        public int VaccineId { get; set; } 
       
       
        public int VaccinationCenterId { get; set; } 

        
    }
}