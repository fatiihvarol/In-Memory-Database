using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApiUnitTest.TestSetup;

public static class User
{
    public static void AddUsers(this UserDbContext context)
    {
        context.Users.AddRange(
            new WebApi.Entities.User { Name = "Fatih", Age = "25", JobId = 1 },
            new WebApi.Entities.User { Name = "Ceren", Age = "18", JobId = 2 },
            new WebApi.Entities.User { Name = "Yavuz", Age = "22", JobId = 3 }
        );
        context.SaveChanges();
    }
}