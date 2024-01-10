using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UserOperations.GetUserQuery
{
    public class GetUserQuery
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserQuery(IUserDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserViewModel> Handle()
        {
            var users = _dbContext.Users.ToList();
            List<UserViewModel> vm = _mapper.Map<List<UserViewModel>>(users);

            return vm;
        }



    }
    public class UserViewModel()
    {
        public string? Name { get; set; }
        public string? Job { get; set; }
        public string? Age { get; set; }
    }
}
