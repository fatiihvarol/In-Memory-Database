using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly IUserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public UpdateUserModel UpdateUserModel { get; set; }
        public int UserId { get; set; }

        public UpdateUserCommand(IUserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
            UpdateUserModel = new UpdateUserModel();
        }
        public void Handle()
        {
            var user = _userDbContext.Users.FirstOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new Exception("user does not exist to update");

            _mapper.Map(UpdateUserModel, user); // Update the properties of the existing user

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
