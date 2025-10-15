using Common.Enums;

namespace Data.Domain.Account;

public class ClinicAdminAccount(string Username, string Email, string PasswordHash) : Account(Username, Email, PasswordHash)
{
    public AccountType Type { get; private set; } = AccountType.ClinicAdmin;

    public ISet<Clinic> Clinics { get; private set; } = new HashSet<Clinic>();
}
