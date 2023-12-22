using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly UserDbContext _userDbContext;
        public UpdateUserModel? UpdateUserModel { get; set; }
        public int UserId { get; set; }

        public UpdateUserCommand(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public void Handle()
        {
            var user = _userDbContext.Users.FirstOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new Exception("user does no exist to update");

            user.Age = UpdateUserModel.Age;
            user.JobId = UpdateUserModel.JobId;
            user.Name = UpdateUserModel.Name;

            _userDbContext.Users.Update(user);
            _userDbContext.SaveChanges();


        }

    }

    public class UpdateUserModel
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
        public int JobId { get; set; }
    }
}
