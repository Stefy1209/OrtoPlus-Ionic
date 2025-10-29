namespace Business.Auth.Models;

public class SignupRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
