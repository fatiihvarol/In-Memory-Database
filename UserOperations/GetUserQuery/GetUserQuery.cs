using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UserOperations.GetUserQuery
{
    public class GetUserQuery{
        private readonly UserDbContext _dbContext;

        public GetUserQuery(UserDbContext dbContext){
            _dbContext=dbContext;
        }

        public List<UserViewModel> Handle(){
          var users = _dbContext.Users.ToList();
          List<UserViewModel> vm = new List<UserViewModel>();


          foreach (var user in users)
          {
            vm.Add(new UserViewModel(){
                Name=user.Name,
                Age=user.Age,
                Job=((JobEnum)user.JobId).ToString()
            });
            
          }
          return vm;
        }


        
    }
    public class UserViewModel(){
        public string? Name { get; set; }
        public string? Job { get; set; }
        public string? Age { get; set; }
    }
}
