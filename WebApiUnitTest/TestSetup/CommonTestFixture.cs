using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;
using WebApiUnitTest.TestSetup;

public class CommonTestFixture
{
    public UserDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var option = new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase("UserDbContextTest").Options;
        Context = new UserDbContext(option);
        Context.Database.EnsureCreated();
        
        Context.AddUsers();
        Context.AddJob();
        Context.SaveChanges();

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        // Assign the created mapper to the property
        Mapper = config.CreateMapper();
    }
}