using Business.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrtoPlusWebAPI.Controllers
{
    [Route("clinics")]
    [ApiController]
    public class ClinicController(IClinicService clinicService) : ControllerBase
    {
        private readonly IClinicService _clinicService = clinicService;

        [HttpGet]
        public async Task<IActionResult> GetClinics()
        {
            var clinicDtos = await _clinicService.GetAllAsync();
            if (clinicDtos == null || !clinicDtos.Any())
            {
                return NotFound("No clinics found.");
            }
            return Ok(clinicDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClinicById(Guid id)
        {
            var clinicDto = await _clinicService.GetByIdAsync(id);
            if (clinicDto == null)
            {
                return NotFound($"Clinic not found.");
            }
            return Ok(clinicDto);
        }
    }
}
