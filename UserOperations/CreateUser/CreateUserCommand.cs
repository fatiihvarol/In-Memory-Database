using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public CreateUserModel Model = new CreateUserModel();

        public CreateUserCommand(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Check if a user with the same properties already exists
            var user = _userDbContext.Users.FirstOrDefault(u => u.Name == Model.Name);

            if (user != null)
                throw new InvalidDataException("User with the same properties already exists");

            user = _mapper.Map<User>(Model);
            /*user = new User
            {
                Age = Model.Age,
                JobId = Model.JobId,
                Name = Model.Name
            };*/

            _userDbContext.Users.Add(user);
            _userDbContext.SaveChanges();

            // Return a success response
        }

    }
    public class CreateUserModel
    {
        public string? Name { get; set; }

        public string? Age { get; set; }

        public int JobId { get; set; }
    }
}
