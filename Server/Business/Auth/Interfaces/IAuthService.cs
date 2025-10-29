using Business.Auth.Models;

namespace Business.Auth.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> SignupAsync(SignupRequest request); 
}
