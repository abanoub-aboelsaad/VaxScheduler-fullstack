using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Reservation;
using api.Models;
using api.Interfaces;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDBContext _context;

        public ReservationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MakeReservationAsync(ReservationDto reservationDto)
        {
            try
            {
                // Validate reservation data
                if (reservationDto == null)
                {
                    return new BadRequestObjectResult("Reservation data is missing");
                }


                // Check if the vaccine exists
                var vaccine = await _context.Vaccines.FindAsync(reservationDto.VaccineId);
                if (vaccine == null)
                {
                    return new BadRequestObjectResult("Invalid vaccine");
                }

                // Check if the vaccination center exists
                var vaccinationCenter = await _context.VaccinationCenters.FindAsync(reservationDto.VaccinationCenterId);
                if (vaccinationCenter == null)
                {
                    return new BadRequestObjectResult("Invalid vaccination center");
                }

                // Check if the user exists
                var user = await _context.Users.FindAsync(reservationDto.AppUserId);
                if (user == null)
                {
                    return new BadRequestObjectResult("Invalid user");
                }

                // Check if the dose number is valid
                if (reservationDto.DoseNumber != "FirstDose" && reservationDto.DoseNumber != "SecondDose")
                {
                    return new BadRequestObjectResult("Invalid dose number reservation");
                }

                // Check if the patient has already made a reservation for the first dose
                var existingFirstDoseReservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.AppUserId == reservationDto.AppUserId && r.DoseNumber == "FirstDose");

                if (reservationDto.DoseNumber == "FirstDose" && existingFirstDoseReservation != null)
                {
                    return new BadRequestObjectResult("Patient already has a reservation for the first dose");
                }

                // Check if the patient has made a reservation for the first dose and it has been approved
                   var existingSecondDoseReservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.AppUserId == reservationDto.AppUserId && r.DoseNumber == "SecondDose");

                if (reservationDto.DoseNumber == "SecondDose" && existingSecondDoseReservation != null)
                {
                    return new BadRequestObjectResult("Patient already has a reservation for the second dose");
                }

                if (reservationDto.DoseNumber == "SecondDose")
                {
                    if (existingFirstDoseReservation == null || existingFirstDoseReservation.Status != "Approved")
                    {
                        return new BadRequestObjectResult("Patient must have an approved reservation for the first dose before reserving the second dose");
                    }

                    // Ensure that the reservation date for the second dose is after the gap period described in the vaccine
                    var gapPeriod = 3;
                    var minSecondDoseDate = existingFirstDoseReservation.ReservationDate.AddDays(gapPeriod);
                    if (reservationDto.ReservationDate < minSecondDoseDate)
                    {
                        return new BadRequestObjectResult($"Reservation date for the second dose must be after {gapPeriod} days from the first dose");
                    }
                }

                // Create the reservation
                var reservation = new Reservation
                {
                    DoseNumber = reservationDto.DoseNumber,
                    ReservationDate = reservationDto.ReservationDate,
                    Status = "Pending",
                    AppUserId = reservationDto.AppUserId,
                    VaccineId = reservationDto.VaccineId,
                    VaccinationCenterId = reservationDto.VaccinationCenterId
                };

                // Save the reservation to the database
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new { ReservationId = reservation.Id, Message = "Reservation successful" });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> ApproveReservationAsync(int id)
        {
            try
            {
                // Find the reservation by id
                var reservation = await _context.Reservations.FindAsync(id);
                if (reservation == null)
                {
                    return new NotFoundObjectResult("Reservation not found");
                }

                // Check if the reservation status is already approved
                if (reservation.Status == "Approved")
                {
                    return new BadRequestObjectResult("Reservation is already approved");
                }

                // Update the reservation status to approved
                reservation.Status = "Approved";

                // Save changes to the database
                await _context.SaveChangesAsync();

                return new OkObjectResult(new { ReservationId = reservation.Id, Message = "Reservation approved" });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return new StatusCodeResult(500);
            }
        }
    }
}
