using Common.Enums;

namespace Data.Domain.Account;

public abstract class Account
{
    public Guid AccountId { get; init; }
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public abstract AccountType AccountType { get;}

    // EF Core
    protected Account() { }

    protected Account(string username, string email, string passwordHash)
    {
        ValidateUsername(username);
        ValidateEmail(email);
        ValidatePasswordHash(passwordHash);

        AccountId = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }

    public void UpdateUsername(string newUsername)
    {
        ValidateUsername(newUsername);
        Username = newUsername;
    }

    public void UpdateEmail(string newEmail)
    {
        ValidateEmail(newEmail);
        Email = newEmail;
    }

    public void UpdatePasswordHash(string newPasswordHash)
    {
        ValidatePasswordHash(newPasswordHash);
        PasswordHash = newPasswordHash;
    }

    private static void ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty or whitespace.", nameof(username));
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty or whitespace.", nameof(email));
    }
    
    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("PasswordHash cannot be empty or whitespace.", nameof(passwordHash));
    }
}
