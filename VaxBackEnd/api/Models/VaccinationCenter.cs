using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class VaccinationCenter
    {
         public int Id { get; set; } // Primary Key

    public string Name { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;

    public string ContactInfo { get; set; } = String.Empty;


   public required string ManagerId { get; set; } 
        public AppUser AppUser { get; set; } 

public ICollection<Vaccine>? Vaccines { get; set; }  = new List<Vaccine>();
 public List<Reservation> Reservations { get; set; } = new List<Reservation>();


    }
}




