using Microsoft.EntityFrameworkCore;
using RestApi.Database;

namespace RestApi.CustomExtensions;

public class CustomDatabaseConfiguration
{
    private readonly IConfiguration config;

    public CustomDatabaseConfiguration(IConfiguration config)
    {
        this.config = config;
    }

    public void ConfigureDatabase(IServiceCollection services)
    {
        var databaseProvider = this.config["DatabaseType"];

        switch (databaseProvider?.ToLower())
        {
            case "sqlite":
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlite(this.config.GetConnectionString("DefaultConnection")));
                break;
            default:
            {
                throw new Exception("Database type not recognized!");
            }
        }
    }
}