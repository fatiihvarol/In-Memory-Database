using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.DeleteUserCommand;
using WebApi.UserOperations.GetBookDetail;
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
            GetUserQuery getUserQuery = new GetUserQuery(_dbContext);
            var users = getUserQuery.Handle();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(_dbContext);
            getUserDetailQuery.UserId = id;
            try
            {
                var user = getUserDetailQuery.Handle();
                return Ok(user);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel createUserModel)
        {
            CreateUserCommand createUserCommand = new CreateUserCommand(_dbContext, _mapper);

            try
            {
                createUserCommand.Model = createUserModel;
                createUserCommand.Handle();
                return Ok(createUserModel);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand(_dbContext)
            { UserId = id };

            try
            {
                deleteUserCommand.Handle();
                return Ok("User deleted");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] UpdateUserModel updateUserModel, int id)
        {
            UpdateUserCommand updateUserCommand = new UpdateUserCommand(_dbContext);
            try
            {
                updateUserCommand.UpdateUserModel = updateUserModel;
                updateUserCommand.UserId = id;
                updateUserCommand.Handle();
                return Ok("User updated");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

    }

}
