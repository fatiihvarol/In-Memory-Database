using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        private readonly IUserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public CreateUserModel Model { get; set; }

        public CreateUserCommand(IUserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
            Model = new CreateUserModel();

        }

        public void Handle()
        {
            if (Model == null)
                throw new ArgumentNullException(nameof(Model), "Model is null");

            var user = _userDbContext.Users.FirstOrDefault(u => u.Name == Model.Name);

            if (user != null)
                throw new InvalidDataException("User with the same properties already exists");

       

            user = _mapper.Map<User>(Model);
            user = _mapper.Map<User>(Model);


            _userDbContext.Users.Add(user);
            _userDbContext.SaveChanges();


        }

    }
    public class CreateUserModel
    {
        public string? Name { get; set; }

        public string? Age { get; set; }

        public int JobId { get; set; }
    }
}
