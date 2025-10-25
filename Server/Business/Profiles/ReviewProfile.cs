using AutoMapper;
using Business.DTOs;
using Data.Domain;

namespace Business.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>();
    }
}
