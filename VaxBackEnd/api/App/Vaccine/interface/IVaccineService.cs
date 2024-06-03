using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Vaccine;

namespace api.Interfaces
{
    public interface IVaccineService
    {
        Task<IEnumerable<VaccineDto>> GetAllVaccinesAsync();
        Task<VaccineDto> GetVaccineByIdAsync(int id);
        Task<VaccineDto> AddVaccineAsync(CreateVaccineRequestDto dto);
        Task<VaccineDto> UpdateVaccineAsync(int id, UpdateVaccineRequestDto dto);
        Task<bool> DeleteVaccineAsync(int id);
        Task<IEnumerable<VaccineDto>> GetAllVaccinesByCenterIdAsync(int centerId);
    }
}
