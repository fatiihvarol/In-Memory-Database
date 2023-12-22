using AutoMapper;
using WebApi.UserOperations.CreateUser;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserModel, User>();
        }

    }


}
