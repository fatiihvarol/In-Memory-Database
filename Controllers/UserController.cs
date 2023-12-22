using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.GetBookDetail;
using WebApi.UserOperations.GetUserQuery;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserDbContext _dbContext; // Inject your DbContext

        public UserController(ILogger<UserController> logger, UserDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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
            CreateUserCommand createUserCommand = new CreateUserCommand(_dbContext);

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
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser is null)
            {
                return Conflict("User not exist");
            }
            _dbContext.Users.Remove(existingUser);
            _dbContext.SaveChanges();
            return Ok();

        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var existuser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existuser is null)
            {
                return Conflict("User does not exist");
            }
            existuser.Age = user.Age;
            existuser.Name = user.Name;
            _dbContext.Users.Update(existuser);
            _dbContext.SaveChanges();
            return Ok();
        }

    }

}
