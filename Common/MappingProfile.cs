using AutoMapper;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.GetUserDetail;
using WebApi.UserOperations.GetUserQuery;
using WebApi.UserOperations.UpdateUser;
using static WebApi.Application.JobOperations.Queries.GetJobDetail.GetJobDetailQuery;
using static WebApi.Application.JobOperations.Queries.GetJobs.GetJobsQuery;
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
            CreateMap<Job, JobsViewModel>();
            CreateMap<Job, JobDetailViewModel>();
        }
    }
}
