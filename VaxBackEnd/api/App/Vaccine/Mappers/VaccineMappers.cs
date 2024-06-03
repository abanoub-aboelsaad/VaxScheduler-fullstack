using api.Dtos.Vaccine;
using api.Models;

namespace api.Mappers
{
    public static class VaccineMappers
    {
        public static VaccineDto ToVaccineDto(this Vaccine vaccineModel)
        {
            return new VaccineDto
            {
                Id = vaccineModel.Id,
                Name = vaccineModel.Name,
                Precautions = vaccineModel.Precautions,
                TimeGapFirstSecondDoseInDays = vaccineModel.TimeGapFirstSecondDoseInDays,
                VaccinationCenterId = vaccineModel.VaccinationCenterId
            };
        }

        public static Vaccine ToVaccineFromCreateDto(this CreateVaccineRequestDto vaccineDto)
        {
            return new Vaccine
            {
                Name = vaccineDto.Name,
                Precautions = vaccineDto.Precautions,
                TimeGapFirstSecondDoseInDays = vaccineDto.TimeGapFirstSecondDoseInDays,
                VaccinationCenterId = vaccineDto.VaccinationCenterId
            };
        }
    }
}
