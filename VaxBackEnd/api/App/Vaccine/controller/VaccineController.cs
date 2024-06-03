using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Vaccine;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/vaccines")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccineController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccines = await _vaccineService.GetAllVaccinesAsync();
            return Ok(vaccines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaccine = await _vaccineService.GetVaccineByIdAsync(id);
            if (vaccine == null)
                return NotFound();
            return Ok(vaccine);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVaccineRequestDto dto)
        {
            var vaccineDto = await _vaccineService.AddVaccineAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = vaccineDto.Id }, vaccineDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVaccineRequestDto dto)
        {
            var vaccineDto = await _vaccineService.UpdateVaccineAsync(id, dto);
            if (vaccineDto == null)
                return NotFound();
            return Ok(vaccineDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _vaccineService.DeleteVaccineAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpGet("by-center/{centerId}")]
        public async Task<IActionResult> GetAllByCenterId(int centerId)
        {
            var vaccines = await _vaccineService.GetAllVaccinesByCenterIdAsync(centerId);
            return Ok(vaccines);
        }
    }
}
