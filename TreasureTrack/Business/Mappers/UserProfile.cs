using AutoMapper;
using TreasureTrack.Business.Entities;
using TreasureTrack.Business.Helpers;
using TreasureTrack.Data.Entities;

namespace TreasureTrack.Business.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHash.CreateHash(src.Password)))
                .ForMember(dest => dest.SuccessfullyPaid, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.SuccessfullyPaid, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore());
        }
    }
}