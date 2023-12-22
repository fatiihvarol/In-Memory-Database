using AutoMapper;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.GetBookDetail;
using WebApi.UserOperations.GetUserQuery;
using WebApi.UserOperations.UpdateUser;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserModel, User>();

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => ((JobEnum)src.JobId).ToString()));

            CreateMap<UpdateUserModel, User>();

            CreateMap<User, UserDetailModelView>()
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => ((JobEnum)src.JobId).ToString()));
        }
    }
}
