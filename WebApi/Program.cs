using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services; // Make sure this is the correct namespace for your DataGenerator


try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddControllers();

// Configure Entity Framework Core with In-Memory Database
    builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseInMemoryDatabase("UserDataBase"));
    builder.Services.AddScoped<IUserDbContext>(provider=>provider.GetService<UserDbContext>());
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

//Logger Service Injection
    builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

    var app = builder.Build();
   app.UseCustomExceptionMiddle();
//app.UseGlobalLoggingMiddleware();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();



// Seed In-Memory Database with Sample Data
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        DataGenerator.Initialize(serviceProvider);
    }

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    throw;
}