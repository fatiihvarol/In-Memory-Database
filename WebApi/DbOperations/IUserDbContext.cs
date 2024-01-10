using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

public interface IUserDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Job>  Jobs { get; set; }

    int SaveChanges();
}