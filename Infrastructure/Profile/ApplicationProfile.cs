using Domain.Dto;
using Domain.Dto.Auth;
using Domain.Dto.User;
using Domain.Entities;

namespace Infrastructure.Profile;

public class ApplicationProfile : AutoMapper.Profile
{
    public ApplicationProfile()
    {
        CreateMap<Register, User>();
        CreateMap<User, GetUserDto>();
        CreateMap<UpdateUserDto, User>();
        
    }
}