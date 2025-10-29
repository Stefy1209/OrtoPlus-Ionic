using System.Security.Claims;
using Business.DTOs;
using Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrtoPlusWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("clinics")]
    public class ClinicController(IClinicService clinicService) : ControllerBase
    {
        private readonly IClinicService _clinicService = clinicService;

        [HttpGet]
        public async Task<IActionResult> GetClinics([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string? filterName, [FromQuery] int? minRating)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var pagedClinics = await _clinicService.GetPagedAsync(pageNumber.Value, pageSize.Value, filterName, minRating);
                if (pagedClinics == null || !pagedClinics.Items.Any())
                {
                    return NotFound("No clinics found for the specified page.");
                }
                return Ok(pagedClinics);
            }

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
            var userId = GetAccountId();
            if(userId == Guid.Empty)
            {
                return Unauthorized("Invalid User");
            }

            var clinicDto = await _clinicService.GetByIdAsync(id, userId);
            if (clinicDto == null)
            {
                return NotFound($"Clinic not found.");
            }
            return Ok(clinicDto);
        }

        [HttpPost("{id}/reviews")]
        public async Task<IActionResult> AddReviewToClinic(Guid id, [FromBody] ReviewDto reviewDto)
        {
            var userId = GetAccountId();
            if(userId == Guid.Empty)
            {
                return Unauthorized("Invalid User");
            }

            try
            {
                var addedReview = await _clinicService.AddReviewAsync(id, userId, reviewDto);
                return CreatedAtAction(nameof(GetClinicById), new { id = id }, addedReview);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Clinic with ID {id} not found.");
            }
        }

        private Guid GetAccountId()
        {
            var accountIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            if (accountIdClaim == null || !Guid.TryParse(accountIdClaim.Value, out var userId))
            {
                return Guid.Empty;
            }

            return userId;
        }
    }
}
