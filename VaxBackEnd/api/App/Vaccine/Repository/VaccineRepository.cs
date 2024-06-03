using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly ApplicationDBContext _context;

        public VaccineRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vaccine>> GetAllVaccinesAsync()
        {
            return await _context.Vaccines.ToListAsync();
        }

        public async Task<Vaccine> GetVaccineByIdAsync(int id)
        {
            return await _context.Vaccines.FindAsync(id);
        }

        public async Task AddVaccineAsync(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVaccineAsync(Vaccine vaccine)
        {
            _context.Vaccines.Update(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVaccineAsync(int id)
        {
            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine != null)
            {
                _context.Vaccines.Remove(vaccine);
                await _context.SaveChangesAsync();
            }
        }
    }
}




