using Common.Enums;

namespace Data.Domain.Account;

public class UserAccount(string Username, string Email, string PasswordHash) : Account(Username, Email, PasswordHash)
{
    public AccountType Type { get; private set; } = AccountType.User;

    public ISet<Review> Reviews { get; private set; } = new HashSet<Review>();
}
