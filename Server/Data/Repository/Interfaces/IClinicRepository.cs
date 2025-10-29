using System.Linq.Expressions;
using Data.Domain;

namespace Data.Repository.Interfaces;

public interface IClinicRepository : IRepository<Clinic, Guid>
{
    Task<int> GetTotalCountAsync(Func<IQueryable<Clinic>, IQueryable<Clinic>>? configureQuery = null ,CancellationToken cancellationToken = default);
}
