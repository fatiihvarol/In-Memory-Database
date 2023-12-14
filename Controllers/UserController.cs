using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.Controllers{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase{
        private readonly ILogger<UserController> _logger;
        private readonly UserDbContext _dbContext; // Inject your DbContext

        public UserController(ILogger<UserController> logger, UserDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            
          var users = _dbContext.Users.ToList();
          return Ok(users);

        }
        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            // Check if a user with the same properties already exists
            var existingUser = _dbContext.Users.FirstOrDefault(u =>
                u.Name == newUser.Name && u.Age == newUser.Age);

            if (existingUser != null)
            {
                return Conflict("User with the same properties already exists");
            }

            // Add the new user to the database
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            // Return a success response
            return Ok(newUser+"User added");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var existingUser = _dbContext.Users.FirstOrDefault(u=> u.Id==id);
            if (existingUser is  null)
            {
                return Conflict("User not exist");
            }
            _dbContext.Users.Remove(existingUser);
            _dbContext.SaveChanges();
            return Ok();

        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody]User user){
            var existuser = _dbContext.Users.FirstOrDefault(u=> u.Id==user.Id);
            if(existuser is  null){
                return Conflict("User does not exist");
            }
                existuser.Age=user.Age;
                existuser.Name=user.Name;
                _dbContext.Users.Update(existuser);
                _dbContext.SaveChanges();
                return Ok();
        }
         
    }

}
