using Microsoft.EntityFrameworkCore;

namespace RestApi.Database;

public class SqliteDatabase : IDatabase
{
    public void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("ConnectionString")));
    }
}