using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IVaccinationCenterRepository
    {
        Task<List<VaccinationCenter>> GetAllVaccinationCentersAsync();
        Task<VaccinationCenter> GetVaccinationCenterByIdAsync(int id);
        Task AddVaccinationCenterAsync(VaccinationCenter vaccinationCenter);
        Task UpdateVaccinationCenterAsync(VaccinationCenter vaccinationCenter);
        Task DeleteVaccinationCenterAsync(int id);

        Task<List<VaccinationCenter>> GetVaccinationCentersByManagerIdAsync(string managerId);
    }
}
