using AutoMapper;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.GetUserQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserModel, User>();
            CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Job, opt => opt.MapFrom(src => ((JobEnum)src.JobId).ToString()));
        }

    }


}
