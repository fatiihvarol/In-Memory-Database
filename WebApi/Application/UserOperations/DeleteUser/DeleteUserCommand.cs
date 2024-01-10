using WebApi.DbOperations;

namespace WebApi.UserOperations.DeleteUserCommand
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        private readonly IUserDbContext _userDbContext;
        public DeleteUserCommand(IUserDbContext userDbContext)
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
