using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IVaccineRepository
    {
        Task<IEnumerable<Vaccine>> GetAllVaccinesAsync();
        Task<Vaccine> GetVaccineByIdAsync(int id);
        Task AddVaccineAsync(Vaccine vaccine);
        Task UpdateVaccineAsync(Vaccine vaccine);
        Task DeleteVaccineAsync(int id);
    }
}
