using AutoMapper;
using Business.DTOs;
using Data.Domain;

namespace Business.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>();
    }
}
