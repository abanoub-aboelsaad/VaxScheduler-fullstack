using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Vaccine;
using api.Interfaces;
using api.Models;
using api.Mappers;

namespace api.Service
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepository;

        public VaccineService(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public async Task<IEnumerable<VaccineDto>> GetAllVaccinesAsync()
        {
            var vaccines = await _vaccineRepository.GetAllVaccinesAsync();
            return vaccines.Select(v => v.ToVaccineDto());
        }

        public async Task<VaccineDto> GetVaccineByIdAsync(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            return vaccine?.ToVaccineDto();
        }

        public async Task<VaccineDto> AddVaccineAsync(CreateVaccineRequestDto vaccineDto)
        {
            var vaccineModel = vaccineDto.ToVaccineFromCreateDto();
            await _vaccineRepository.AddVaccineAsync(vaccineModel);
            return vaccineModel.ToVaccineDto();
        }

        public async Task<VaccineDto> UpdateVaccineAsync(int id, UpdateVaccineRequestDto updateDto)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if (vaccine == null)
                return null;

            vaccine.Name = updateDto.Name;
            vaccine.Precautions = updateDto.Precautions;
            vaccine.TimeGapFirstSecondDoseInDays = updateDto.TimeGapFirstSecondDoseInDays;

            await _vaccineRepository.UpdateVaccineAsync(vaccine);
            return vaccine.ToVaccineDto();
        }

        public async Task<bool> DeleteVaccineAsync(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if (vaccine == null)
                return false;

            await _vaccineRepository.DeleteVaccineAsync(id);
            return true;
        }


 public async Task<IEnumerable<VaccineDto>> GetAllVaccinesByCenterIdAsync(int centerId)
        {
            var vaccines = await _vaccineRepository.GetAllVaccinesAsync();
            return vaccines.Where(v => v.VaccinationCenterId == centerId).Select(v => v.ToVaccineDto());
        }

    }
}
