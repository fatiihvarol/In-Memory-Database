using WebApi.DbOperations;

namespace WebApiUnitTest.TestSetup;

public static class Job
{
    public static void AddJob(this UserDbContext context)
    {
        context.Jobs.AddRange(
            new WebApi.Entities.Job
            {
                Name = "Computer Engineer",
            },
            new WebApi.Entities.Job
            {
                Name = "Security Analyst",
            },
            new WebApi.Entities.Job
            {
                Name = "Data Analyst",
            },
            new WebApi.Entities.Job
            {
                Name = "Doctor",
            }
        );
        context.SaveChanges();
    }
}