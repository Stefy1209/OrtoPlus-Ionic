using Business.Auth.Interfaces;
using Business.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace OrtoPlusWebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = "An error occurred during login." });
        }
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        try
        {
            var response = await _authService.SignupAsync(request);
            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = "An error occurred during signup." });
        }
    }
}
