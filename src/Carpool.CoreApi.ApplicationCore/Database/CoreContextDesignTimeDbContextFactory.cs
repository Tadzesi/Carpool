#if DEBUG
using Carpool.CoreApi.ApplicationCore.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Carpool.CoreApi.Api.Database
{
    public class CoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoreContext>
    {
        public CoreContext CreateDbContext(string[] args)
        {
            string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            string? connectionString = configuration.GetConnectionString("MicrosoftSQLConnection");
            DbContextOptionsBuilder<CoreContext> builder = new DbContextOptionsBuilder<CoreContext>().UseSqlServer(
                connectionString);

            (configuration as ConfigurationRoot)?.Dispose();

            return new CoreContext(builder.Options);
        }
    }
}
#endif
