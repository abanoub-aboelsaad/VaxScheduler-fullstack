namespace api.Dtos.Reservation
{
    public class ReservationWithVaccineDto
    {
      public int Id { get; set; }
        public string DoseNumber { get; set; }
        public DateTime ReservationDate { get; set; }
        public string AppUserId { get; set; }
        public string UserName { get; set; }
        public int VaccineId { get; set; }
        public string VaccineName { get; set; }
        public int VaccinationCenterId { get; set; }
    }
}
