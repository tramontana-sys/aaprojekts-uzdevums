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
                services.AddSingleton<IDatabase, SqliteDatabase>();
                break;
            default:
            {
                throw new Exception("Database type not recognized!");
            }
        }
        
        var database = services.BuildServiceProvider().GetService<IDatabase>();
        database?.ConfigureDatabase(services, this.config);
    }
}