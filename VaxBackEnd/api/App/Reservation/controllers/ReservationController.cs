using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Reservation;
using api.Services;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Data; // Import EntityFrameworkCore for accessing database context

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ApplicationDBContext _context; // Inject database context

        public ReservationController(IReservationService reservationService, ApplicationDBContext context)
        {
            _reservationService = reservationService;
            _context = context; // Assign injected database context
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationDto reservationDto)
        {
            try
            {
                return await _reservationService.MakeReservationAsync(reservationDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while processing the reservation");
            }
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveReservation(int id)
        {
            try
            {
                return await _reservationService.ApproveReservationAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while approving the reservation");
            }
        }

   [HttpGet("all/reservations")]
public async Task<IActionResult> GetAllPendingReservationsWithVaccineAndUserName()
{
    try
    {
        var pendingReservationsWithVaccineAndUserName = await (from reservation in _context.Reservations
                                                                where reservation.Status == "Pending" // Assuming ReservationStatus is an enum representing reservation status
                                                                join vaccine in _context.Vaccines on reservation.VaccineId equals vaccine.Id
                                                                join user in _context.Users on reservation.AppUserId equals user.Id
                                                                select new ReservationWithVaccineDto
                                                                {
                                                                    Id = reservation.Id,
                                                                    DoseNumber = reservation.DoseNumber,
                                                                    ReservationDate = reservation.ReservationDate,
                                                                    AppUserId = reservation.AppUserId,
                                                                    UserName = user.UserName,
                                                                    VaccineId = reservation.VaccineId,
                                                                    VaccineName = vaccine.Name,
                                                                    VaccinationCenterId = reservation.VaccinationCenterId
                                                                }).ToListAsync();

        return Ok(pendingReservationsWithVaccineAndUserName);
    }
    catch (Exception ex)
    {
        // Log the exception
        Console.WriteLine(ex);
        return StatusCode(500, "An error occurred while retrieving pending reservations");
    }
}



  [HttpGet("all/users/with-approved-second-dose")]
        public async Task<IActionResult> GetAllUsersWithApprovedSecondDose()
        {
            try
            {
                // Retrieve all users with approved second dose reservations using navigation properties
                var usersWithApprovedSecondDose = await _context.Users
                    .Include(user => user.Reservations)
                    .Where(user => user.Reservations.Any(reservation => reservation.DoseNumber == "SecondDose" && reservation.Status == "Approved"))
                    .ToListAsync();

                return Ok(usersWithApprovedSecondDose);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while retrieving users with approved second dose reservations");
            }
        }
    }

    }

