using Common.Enums;

namespace Business.Auth.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email, AccountType accountType);
}
