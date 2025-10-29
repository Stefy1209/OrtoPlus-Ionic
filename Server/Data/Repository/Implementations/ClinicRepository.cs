using System.Linq.Expressions;
using Data.Domain;
using Data.Persistence;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations;

public class ClinicRepository(ApplicationDbContext context) : IClinicRepository
{
    private readonly ApplicationDbContext _context = context;

    public Task<Clinic> AddAsync(Clinic entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Clinic>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<Clinic, object>>[] includes)
    {
        IQueryable<Clinic> query = _context.Set<Clinic>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Clinic>> GetAllAsync(Func<IQueryable<Clinic>, IQueryable<Clinic>>? configureQuery = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Clinic> query = _context.Set<Clinic>();

        if (configureQuery != null)
        {
            query = configureQuery(query);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Clinic?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params Expression<Func<Clinic, object>>[] includes)
    {
        IQueryable<Clinic> query = _context.Set<Clinic>().Where(c => c.ClinicId == id);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Clinic?> GetByIdAsync(Guid id, Func<IQueryable<Clinic>, IQueryable<Clinic>>? configureQuery = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Clinic> query = _context.Set<Clinic>().Where(c => c.ClinicId == id);

        if (configureQuery != null)
        {
            query = configureQuery(query);
        }

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(Func<IQueryable<Clinic>, IQueryable<Clinic>>? configureQuery = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Clinic> query = _context.Set<Clinic>();

        if (configureQuery != null)
        {
            query = configureQuery(query);
        }

        return query.Count();
    }

    public async Task<Clinic> UpdateAsync(Clinic entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Clinic>().Update(entity);
        var noUpdated = await _context.SaveChangesAsync(cancellationToken);
        if(noUpdated == 0)
        {
            throw new Exception("Update failed.");
        }
        return entity;
    }
}
