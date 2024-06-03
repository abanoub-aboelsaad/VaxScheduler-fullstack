using System;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Reservation;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDBContext _context;

        public ReservationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            return MapToDto(reservation);
        }

        public async Task AddReservationAsync(ReservationDto reservationDto)
        {
            var reservation = MapToEntity(reservationDto);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(int id, ReservationDto reservationDto)
        {
            var existingReservation = await _context.Reservations.FindAsync(id);
            if (existingReservation == null)
            {
                throw new InvalidOperationException("Reservation not found");
            }

            existingReservation.DoseNumber = reservationDto.DoseNumber;
            existingReservation.ReservationDate = reservationDto.ReservationDate;
            existingReservation.AppUserId = reservationDto.AppUserId;
            existingReservation.VaccineId = reservationDto.VaccineId;
            existingReservation.VaccinationCenterId = reservationDto.VaccinationCenterId;

            _context.Entry(existingReservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation not found");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        // Helper method to map Reservation entity to ReservationDto
        private ReservationDto MapToDto(Reservation reservation)
        {
            return new ReservationDto
            {
                DoseNumber = reservation.DoseNumber,
                ReservationDate = reservation.ReservationDate,
                AppUserId = reservation.AppUserId,
                VaccineId = reservation.VaccineId,
                VaccinationCenterId = reservation.VaccinationCenterId
            };
        }

        // Helper method to map ReservationDto to Reservation entity
        private Reservation MapToEntity(ReservationDto reservationDto)
        {
            return new Reservation
            {
                DoseNumber = reservationDto.DoseNumber,
                ReservationDate = reservationDto.ReservationDate,
                AppUserId = reservationDto.AppUserId,
                VaccineId = reservationDto.VaccineId,
                VaccinationCenterId = reservationDto.VaccinationCenterId
            };
        }
    }
}
