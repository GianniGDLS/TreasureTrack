using AutoMapper;
using TreasureTrack.Business.Entities;
using TreasureTrack.Controllers.Contracts.V1;

namespace TreasureTrack.Controllers.Mappers
{
    public class UserControllerProfile : Profile
    {
        public UserControllerProfile()
        {
            CreateMap<RegisterUserRequest, CreateUserDto>();
            CreateMap<UpdateUserRequest, UpdateUserDto>();
            CreateMap<UserDto, GetUserResponse>();
            CreateMap<StageDto, GetStageResponse>();
            CreateMap<AttemptDto, GetAttemptResponse>();
        }
    }
}