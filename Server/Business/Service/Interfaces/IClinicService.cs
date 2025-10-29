using Business.DTOs;
using Common;

namespace Business.Service.Interfaces;

public interface IClinicService : IService<ClinicDto, Guid>
{
    Task<PageMetadata<ClinicDto>> GetPagedAsync(int pageNumber, int pageSize, string? filterName, int? minRating, CancellationToken cancellationToken = default);
    Task<ReviewDto> AddReviewAsync(Guid clinicId, Guid userId, ReviewDto reviewDto, CancellationToken cancellationToken = default);
    Task<ClinicDto> GetByIdAsync(Guid clinicId, Guid userId, CancellationToken cancellationToken = default);
}