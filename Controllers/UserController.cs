using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.DeleteUserCommand;
using WebApi.UserOperations.GetUserDetail;
using WebApi.UserOperations.GetUserQuery;
using WebApi.UserOperations.UpdateUser;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IMapper _mapper;
        private readonly UserDbContext _dbContext; // Inject your DbContext

        public UserController(ILogger<UserController> logger, UserDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            GetUserQuery getUserQuery = new GetUserQuery(_dbContext, _mapper);
            var users = getUserQuery.Handle();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(_dbContext, _mapper)
            {
                UserId = id
            };

            GetUserDetailQueryValidation validation = new GetUserDetailQueryValidation();
            validation.ValidateAndThrow(getUserDetailQuery);
            var user = getUserDetailQuery.Handle();
            return Ok(user);



        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel createUserModel)
        {



            CreateUserCommand createUserCommand = new CreateUserCommand(_dbContext, _mapper)
            {
                Model = createUserModel
            };
            CreateUserCommandValidator validation = new CreateUserCommandValidator();

            validation.ValidateAndThrow(createUserCommand);

            createUserCommand.Handle();
            return Ok(createUserModel);


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand(_dbContext)
            { UserId = id };


            DeleteUserCommandValidator validation = new DeleteUserCommandValidator();
            validation.ValidateAndThrow(deleteUserCommand);
            deleteUserCommand.Handle();
            return Ok("User deleted");



        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] UpdateUserModel updateUserModel, int id)
        {
            UpdateUserCommand updateUserCommand = new UpdateUserCommand(_dbContext, _mapper);

            UpdateUserCommandValidation validation = new UpdateUserCommandValidation();
            updateUserCommand.UpdateUserModel = updateUserModel;
            updateUserCommand.UserId = id;
            validation.ValidateAndThrow(updateUserCommand);
            updateUserCommand.Handle();
            return Ok("User updated");



        }

    }

}
