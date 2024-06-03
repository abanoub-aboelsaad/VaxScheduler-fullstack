using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.VaccinationCenter;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/VaccinationCenter")]
    [ApiController]
    public class VaccinationCenterController : ControllerBase 
    {
        private readonly IVaccinationCenterService _vaccinationCenterService;

        public VaccinationCenterController(IVaccinationCenterService vaccinationCenterService)
        {
            _vaccinationCenterService = vaccinationCenterService;
        }


//  [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccinationCenters = await _vaccinationCenterService.GetAllVaccinationCentersAsync();
            return Ok(vaccinationCenters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var vaccinationCenter = await _vaccinationCenterService.GetVaccinationCenterByIdAsync(id);
            if (vaccinationCenter == null)
                return NotFound();
            return Ok(vaccinationCenter);
        }
 [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVaccinationCenterRequestDto vaccinationCenterDto)
        {
            var createdVaccinationCenter = await _vaccinationCenterService.AddVaccinationCenterAsync(vaccinationCenterDto);
            return CreatedAtAction(nameof(GetOne), new { id = createdVaccinationCenter.Id }, createdVaccinationCenter);
        }


//  [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVaccinationCenterRequestDto updateDto)
        {
            var updatedVaccinationCenter = await _vaccinationCenterService.UpdateVaccinationCenterAsync(id, updateDto);
            if (updatedVaccinationCenter == null)
                return NotFound();
            return Ok(updatedVaccinationCenter);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _vaccinationCenterService.DeleteVaccinationCenterAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

  [HttpGet("by-manager/{managerId}")]
        public async Task<IActionResult> GetByManagerId(string managerId)
        {
            var vaccinationCenters = await _vaccinationCenterService.GetVaccinationCentersByManagerIdAsync(managerId);
            return Ok(vaccinationCenters);
        }


        
    }
}
