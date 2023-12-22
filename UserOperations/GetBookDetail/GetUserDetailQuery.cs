using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UserOperations.GetBookDetail
{
    public class GetUserDetailQuery
    {
        private readonly UserDbContext _userDbContext;

        public int UserId { get; set; }
        public GetUserDetailQuery(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public UserDetailModelView Handle()
        {
            var existingUser = _userDbContext.Users.FirstOrDefault(u => u.Id == UserId) ?? throw new Exception("User not exist");

            UserDetailModelView userDetailModelView = new UserDetailModelView
            {
                Name = existingUser.Name,
                Age = existingUser.Age,
                Job = ((JobEnum)existingUser.JobId).ToString()
            };
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
