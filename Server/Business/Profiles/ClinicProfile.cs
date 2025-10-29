using AutoMapper;
using Business.DTOs;
using Data.Domain;

namespace Business.Profiles;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicDto>();
        CreateMap<ClinicDto, Clinic>();
    }
}
