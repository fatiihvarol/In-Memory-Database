using AutoMapper;
using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UserOperations.GetUserDetail
{
    public class GetUserDetailQuery
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public int UserId { get; set; }

        public GetUserDetailQuery(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public UserDetailModelView Handle()
        {
            var existingUser = _userDbContext.Users.FirstOrDefault(u => u.Id == UserId) ?? throw new Exception("User not exist");
            UserDetailModelView userDetailModelView = _mapper.Map<UserDetailModelView>(existingUser);
            return userDetailModelView;
        }
    }

    public class UserDetailModelView
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Job { get; set; }
    }
}
