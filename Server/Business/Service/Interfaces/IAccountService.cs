using Data.Domain.Account;

namespace Business.Service.Interfaces;

public interface IAccountService
{
    Task<Account> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Account> RegisterAsync(string email, string password, CancellationToken cancellationToken = default);
}
