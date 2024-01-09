using WebApi.DbOperations;

namespace WebApi.UserOperations.DeleteUserCommand
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        private readonly UserDbContext _userDbContext;
        public DeleteUserCommand(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void Handle()
        {
            var user = _userDbContext.Users.FirstOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new Exception("user does not exist");

            _userDbContext.Users.Remove(user);
            _userDbContext.SaveChanges();

        }


    }
}
