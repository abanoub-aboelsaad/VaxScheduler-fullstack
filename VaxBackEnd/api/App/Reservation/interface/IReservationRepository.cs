using System.Threading.Tasks;
using api.Dtos.Reservation;

namespace api.Interfaces
{
    public interface IReservationRepository
    {
        Task<ReservationDto> GetReservationByIdAsync(int id);
        
        Task AddReservationAsync(ReservationDto reservationDto);
        
        Task UpdateReservationAsync(int id, ReservationDto reservationDto);
        
        Task DeleteReservationAsync(int id);
    }
}
