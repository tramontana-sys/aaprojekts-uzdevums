using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}