using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context =
                new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>());
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Jobs.AddRange(
                    new Job
                    {
                        Name = "Computer Engineer",
                    },
                    new Job
                    {
                        Name = "Security Analyst",
                    },
                    new Job
                    {
                        Name = "Data Analyst",
                    },
                    new Job
                    {
                        Name = "Doctor",
                    }
                );
                context.Users.AddRange(
                    new User { Name = "Fatih", Age = "25", JobId = 1 },
                    new User { Name = "Ceren", Age = "18", JobId = 2 },
                    new User { Name = "Yavuz", Age = "22", JobId = 3 }
                );

                context.SaveChanges();
            }
        }
    }
}