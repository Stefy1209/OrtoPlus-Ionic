using Business.Service.Interfaces;
using Data.Domain.Account;
using Data.Repository.Interfaces;

namespace Business.Service.Implementations;

public class AccountService(IAccountRepository repository) : IAccountService
{
    private readonly IAccountRepository _repository = repository;

    public async Task<Account> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var account = await _repository.GetByEmailAsync(email, cancellationToken) ?? throw new Exception("Account not found.");
        if (account.PasswordHash != password)
        {
            throw new Exception("Invalid password.");
        }
        return account;        
    }

    public Task<Account> RegisterAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}