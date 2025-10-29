using AutoMapper;
using Business.DTOs;
using Business.Hubs;
using Business.Service.Interfaces;
using Common;
using Data.Domain;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Business.Service.Implementations;

public class ClinicService(IClinicRepository clinicRepository, IMapper mapper, IHubContext<NotificationHub, INotificationClient> hubContext) : IClinicService
{
    private readonly IClinicRepository _clinicRepository = clinicRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext = hubContext;

    public async Task<ReviewDto> AddReviewAsync(Guid clinicId, Guid userId, ReviewDto reviewDto, CancellationToken cancellationToken = default)
    {
        var review = _mapper.Map<Review>(reviewDto);
        review.UserAccountId = userId;
        var clinic = await _clinicRepository.GetByIdAsync(clinicId, cancellationToken, c => c.Reviews) ?? throw new KeyNotFoundException($"Clinic with ID {clinicId} not found.");
        clinic.AddReview(review);
        await _clinicRepository.UpdateAsync(clinic, cancellationToken);
        var createdReview = _mapper.Map<ReviewDto>(review);
        await _hubContext.Clients.User(userId.ToString()).ReceiveNotification($"Notification \"{reviewDto.Comment}\" was created");
        return createdReview;
    }

    public async Task<IEnumerable<ClinicDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var clinics = await _clinicRepository.GetAllAsync(cancellationToken, c => c.Address);
        var clinicDtoS = _mapper.Map<IEnumerable<ClinicDto>>(clinics);
        return clinicDtoS;
    }

    public async Task<ClinicDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var clinic = await _clinicRepository.GetByIdAsync(id, cancellationToken, c => c.Address, c => c.Reviews);
        var clinicDto = _mapper.Map<ClinicDto>(clinic);
        return clinicDto;
    }

    public async Task<ClinicDto> GetByIdAsync(Guid clinicId, Guid userId, CancellationToken cancellationToken = default)
    {
        var clinic = await _clinicRepository.GetByIdAsync(
            clinicId,
            query => query
                .Include(c => c.Address)
                .Include(c => c.Reviews.Where(r => r.UserAccountId == userId)),
            cancellationToken);
        var clinicDto = _mapper.Map<ClinicDto>(clinic);
        return clinicDto;
    }

    public async Task<PageMetadata<ClinicDto>> GetPagedAsync(int pageNumber, int pageSize, string? filterName, int? minRating, CancellationToken cancellationToken = default)
    {
        IQueryable<Clinic> baseQuery(IQueryable<Clinic> query)
        {
            if (!string.IsNullOrWhiteSpace(filterName))
                query = query.Where(c => c.Name.ToLower().Contains(filterName.ToLower()));

            if (minRating.HasValue)
                query = query.Where(c => c.Rating >= minRating.Value);

            return query
                .OrderBy(c => c.Name)
                .Include(c => c.Address);
        }

        var clinics = await _clinicRepository.GetAllAsync(
            query => baseQuery(query)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize),
            cancellationToken);

        var totalCount = await _clinicRepository.GetTotalCountAsync(baseQuery, cancellationToken);

        var clinicDtos = _mapper.Map<IEnumerable<ClinicDto>>(clinics);
        var pageMetadata = new PageMetadata<ClinicDto>
        {
            TotalCount = totalCount,
            Items = clinicDtos,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return pageMetadata;
    }
}
