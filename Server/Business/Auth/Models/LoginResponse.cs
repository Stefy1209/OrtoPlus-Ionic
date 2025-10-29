namespace Business.Auth.Models;

public class LoginResponse
{
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
}
