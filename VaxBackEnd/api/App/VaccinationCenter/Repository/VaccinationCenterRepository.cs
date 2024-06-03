using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class VaccinationCenterRepository : IVaccinationCenterRepository
    {
        private readonly ApplicationDBContext _context;

        public VaccinationCenterRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<VaccinationCenter>> GetAllVaccinationCentersAsync()
        {
            return await _context.VaccinationCenters.ToListAsync();
        }

        public async Task<VaccinationCenter> GetVaccinationCenterByIdAsync(int id)
        {
            return await _context.VaccinationCenters.FindAsync(id);
        }

        public async Task AddVaccinationCenterAsync(VaccinationCenter vaccinationCenter)
        {
            _context.VaccinationCenters.Add(vaccinationCenter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVaccinationCenterAsync(VaccinationCenter vaccinationCenter)
        {
            _context.Entry(vaccinationCenter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVaccinationCenterAsync(int id)
        {
            var vaccinationCenter = await _context.VaccinationCenters.FindAsync(id);
            if (vaccinationCenter != null)
            {
                _context.VaccinationCenters.Remove(vaccinationCenter);
                await _context.SaveChangesAsync();
            }
        }


  public async Task<List<VaccinationCenter>> GetVaccinationCentersByManagerIdAsync(string managerId)
        {
            return await _context.VaccinationCenters
                .Where(vc => vc.ManagerId == managerId)
                .ToListAsync();
        }

    }
}
