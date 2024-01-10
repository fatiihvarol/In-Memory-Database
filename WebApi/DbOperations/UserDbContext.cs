using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations{
    public class UserDbContext : DbContext,IUserDbContext{

        public UserDbContext(DbContextOptions<UserDbContext> options):base(options){
            
        }

      

        public DbSet<User> Users {get;set;}
        public DbSet<Job> Jobs {get;set;}
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
