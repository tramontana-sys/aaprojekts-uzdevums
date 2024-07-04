namespace RestApi.Database;

public interface IDatabase
{
    void ConfigureDatabase(IServiceCollection services, IConfiguration configuration);
}