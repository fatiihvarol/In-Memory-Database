using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations{
    public class UserDbContext : DbContext{

        public UserDbContext(DbContextOptions<UserDbContext> options):base(options){
            
        }

      

        public DbSet<User> Users {get;set;}

    }
}
