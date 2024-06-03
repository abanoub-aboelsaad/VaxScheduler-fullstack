using api.Dtos.VaccinationCenter;
using api.Models;

namespace api.Service
{
    public static class VaccinationCenterMappers
    {
        public static VaccinationCenterDto ToVaccinationCenterDto(this VaccinationCenter vaccinationCenterModel)
        {
            return new VaccinationCenterDto
            {
                Id = vaccinationCenterModel.Id,
                Name = vaccinationCenterModel.Name,
                Address = vaccinationCenterModel.Address,
                ContactInfo = vaccinationCenterModel.ContactInfo,
                // AppUserId =vaccinationCenterModel.AppUserId
            };
        }

        public static VaccinationCenter ToVaccinationCenterFromCreateDto(this CreateVaccinationCenterRequestDto vaccinationCenterDto)
        {
            return new VaccinationCenter
            {
                Name = vaccinationCenterDto.Name,
                Address = vaccinationCenterDto.Address,
                ContactInfo = vaccinationCenterDto.ContactInfo,
                ManagerId =vaccinationCenterDto.ManagerId
            };
        }
    }
}
