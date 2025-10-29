using Data.Domain.Account;

namespace Data.Repository.Interfaces;

public interface IAccountRepository : IRepository<Account, Guid>
{
    Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
