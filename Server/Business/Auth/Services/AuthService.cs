using Business.Auth.Interfaces;
using Business.Auth.Models;
using Data.Domain.Account;
using Data.Repository.Interfaces;

namespace Business.Auth.Services;

public class AuthService(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator, JwtSettings jwtSettings) : IAuthService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly JwtSettings _jwtSettings = jwtSettings;

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var account = await _accountRepository.GetByEmailAsync(request.Email) ?? throw new UnauthorizedAccessException("Invalid email or password.");
        if (!BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var LoginResponse = GenerateLoginResponse(account);
        return LoginResponse;
    }

    public async Task<LoginResponse> SignupAsync(SignupRequest request)
    {
        var account = new UserAccount(request.Username, request.Email, BCrypt.Net.BCrypt.HashPassword(request.Password));
        await _accountRepository.AddAsync(account);

        var loginResponse = GenerateLoginResponse(account);
        return loginResponse;
    }

    private LoginResponse GenerateLoginResponse(Account account)
    {
        var token = _jwtTokenGenerator.GenerateToken(account.AccountId.ToString(), account.Email, account.AccountType);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        return new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt
        };
    }
}
