using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Reservation;

namespace api.Interfaces
{
    public interface IReservationService
    {
        Task<IActionResult> MakeReservationAsync(ReservationDto reservationDto);
        
        Task<IActionResult> ApproveReservationAsync(int id);
    }
}
