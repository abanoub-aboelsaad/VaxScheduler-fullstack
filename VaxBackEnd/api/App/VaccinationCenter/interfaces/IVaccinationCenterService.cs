using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.VaccinationCenter;

namespace api.Interfaces
{
    public interface IVaccinationCenterService
    {
        Task<IEnumerable<VaccinationCenterDto>> GetAllVaccinationCentersAsync();
        Task<VaccinationCenterDto> GetVaccinationCenterByIdAsync(int id);
        Task<VaccinationCenterDto> AddVaccinationCenterAsync(CreateVaccinationCenterRequestDto vaccinationCenterDto);
        Task<VaccinationCenterDto> UpdateVaccinationCenterAsync(int id, UpdateVaccinationCenterRequestDto updateDto);
        Task<bool> DeleteVaccinationCenterAsync(int id);

                Task<IEnumerable<VaccinationCenterDto>> GetVaccinationCentersByManagerIdAsync(string managerId);

    }
}
