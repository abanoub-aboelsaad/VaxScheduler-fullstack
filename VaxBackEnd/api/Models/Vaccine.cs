using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Vaccine
    {
        
        public int Id { get; set; } // Primary Key

        public string Name { get; set; } = String.Empty;

        public string Precautions { get; set; } = String.Empty;

        public int TimeGapFirstSecondDoseInDays { get; set; }

        public int VaccinationCenterId { get; set; } 

         public VaccinationCenter? VaccinationCenter { get; set; } 
          public List<Reservation> Reservations { get; set; } = new List<Reservation>();

         
    }
}