using System.Linq.Expressions;
using Data.Domain.Account;
using Data.Persistence;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations;

public class AccountRepository(ApplicationDbContext context) : IAccountRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Account> AddAsync(Account entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<Account>().AddAsync(entity, cancellationToken);
        var modified = await _context.SaveChangesAsync(cancellationToken);
        if (modified == 0)
        {
            throw new Exception("Failed to add account.");
        }
        return entity;
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<Account, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Account>> GetAllAsync(Func<IQueryable<Account>, IQueryable<Account>>? configureQuery = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Set<Account>().FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }

    public Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<Account, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Account?> GetByIdAsync(Guid id, Func<IQueryable<Account>, IQueryable<Account>>? configureQuery = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Account> UpdateAsync(Account entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
