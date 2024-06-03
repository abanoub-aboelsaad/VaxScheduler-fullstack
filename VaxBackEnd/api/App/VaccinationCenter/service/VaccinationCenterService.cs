using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.VaccinationCenter;
using api.Interfaces;
using api.Models;

namespace api.Service
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IVaccinationCenterRepository _vaccinationCenterRepository;

        public VaccinationCenterService(IVaccinationCenterRepository vaccinationCenterRepository)
        {
            _vaccinationCenterRepository = vaccinationCenterRepository;
        }

        public async Task<IEnumerable<VaccinationCenterDto>> GetAllVaccinationCentersAsync()
        {
            var vaccinationCenters = await _vaccinationCenterRepository.GetAllVaccinationCentersAsync();
            return vaccinationCenters.Select(v => v.ToVaccinationCenterDto());
        }

        public async Task<VaccinationCenterDto?> GetVaccinationCenterByIdAsync(int id)
        {
            var vaccinationCenter = await _vaccinationCenterRepository.GetVaccinationCenterByIdAsync(id);
            return vaccinationCenter?.ToVaccinationCenterDto();
        }

        public async Task<VaccinationCenterDto> AddVaccinationCenterAsync(CreateVaccinationCenterRequestDto vaccinationCenterDto)
        {
            var vaccinationCenterModel = vaccinationCenterDto.ToVaccinationCenterFromCreateDto();
            await _vaccinationCenterRepository.AddVaccinationCenterAsync(vaccinationCenterModel);
            return vaccinationCenterModel.ToVaccinationCenterDto();
        }

        public async Task<VaccinationCenterDto> UpdateVaccinationCenterAsync(int id, UpdateVaccinationCenterRequestDto updateDto)
        {
            var vaccinationCenter = await _vaccinationCenterRepository.GetVaccinationCenterByIdAsync(id);
            if (vaccinationCenter == null)
                return null;

            vaccinationCenter.Name = updateDto.Name;
            vaccinationCenter.Address = updateDto.Address;
            vaccinationCenter.ContactInfo = updateDto.ContactInfo;
            //  vaccinationCenter.ManagerId = updateDto.ManagerId;

            await _vaccinationCenterRepository.UpdateVaccinationCenterAsync(vaccinationCenter);
            return vaccinationCenter.ToVaccinationCenterDto();
        }

        public async Task<bool> DeleteVaccinationCenterAsync(int id)
        {
            var vaccinationCenter = await _vaccinationCenterRepository.GetVaccinationCenterByIdAsync(id);
            if (vaccinationCenter == null)
                return false;

            await _vaccinationCenterRepository.DeleteVaccinationCenterAsync(id);
            return true;
        }



        
        public async Task<IEnumerable<VaccinationCenterDto>> GetVaccinationCentersByManagerIdAsync(string managerId)
        {
            var vaccinationCenters = await _vaccinationCenterRepository.GetVaccinationCentersByManagerIdAsync(managerId);
            return vaccinationCenters.Select(vc => vc.ToVaccinationCenterDto());
        }
    }
}
