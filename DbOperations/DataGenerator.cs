using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations{
    public class DataGenerator{
        public static void Initialize(IServiceProvider serviceProvider){
            using var context = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()); 
            {
                if (context.Users.Any())
                {
                    return;
                }
                context.Users.AddRange(
                new User { Name = "Fatih", Age = "25" },
                new User { Name = "Ceren", Age = "30" },
                new User { Name = "Yavuz", Age = "22" }
            );

             context.SaveChanges();
            }

        }

    }
}
