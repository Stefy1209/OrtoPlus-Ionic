using AutoMapper;
using Business.DTOs;
using Business.Hubs;
using Business.Service.Interfaces;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Business.Service.Implementations;

public class ClinicService(IClinicRepository clinicRepository, IMapper mapper, IHubContext<NotificationHub> hubContext) : IClinicService
{
    private readonly IClinicRepository _clinicRepository = clinicRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

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
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Clinic with ID " + id + " was accessed.");
        return clinicDto;
    }
}
