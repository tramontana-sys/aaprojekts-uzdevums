using Microsoft.EntityFrameworkCore;
using RestApi.Database;

namespace RestApi.Tests;

public class DatabaseContextFactory
{
    public static DatabaseContext CreateInMemoryDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new DatabaseContext(options);
    }
}