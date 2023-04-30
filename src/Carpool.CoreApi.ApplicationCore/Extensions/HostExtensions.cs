using Carpool.CoreApi.ApplicationCore.Database.Seeding;
using GuardNet;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Carpool.CoreApi.ApplicationCore.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host, IDbContextSeeder<TContext>? seeder = null)
            where TContext : DbContext
        {
            Guard.NotNull(host, nameof(host));
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;
            ILogger<TContext> logger = provider.GetRequiredService<ILogger<TContext>>();
            TContext context = provider.GetRequiredService<TContext>();

            try
            {
                logger.LogDebug("Migrating database associated with context {context}", typeof(TContext).Name);

                Policy.Handle<SqlException>()
                    .WaitAndRetry(new[] { TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(7) })
                    .Execute(() =>
                    {
                        context.Database.Migrate();
                        seeder?.Seed(context, provider);
                    });

                logger.LogDebug("Successfully migrated database associated with context {context}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while migrating database associated with context {context}: {err}",
                    typeof(TContext).Name,
                    ex.Message);
            }

            return host;
        }

        public static async Task<IHost> MigrateDbContextAsync<TContext>(this IHost host, IAsyncDbContextSeeder<TContext>? seeder = null)
            where TContext : DbContext
        {
            Guard.NotNull(host, nameof(host));
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;
            ILogger<TContext> logger = provider.GetRequiredService<ILogger<TContext>>();

            try
            {
                TContext context = provider.GetRequiredService<TContext>();
                logger.LogDebug("Migrating database associated with context {context}", typeof(TContext).Name);

                await Policy.Handle<SqlException>()
                    .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(7) })
                    .ExecuteAsync(async () =>
                    {
                        await context.Database.MigrateAsync();
                        if (seeder != null)
                        {
                            await seeder.SeedAsync(context, provider);
                        }
                    });

                logger.LogDebug("Successfully migrated database associated with context {context}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while migrating database associated with context {context}: {err}",
                    typeof(TContext).Name,
                    ex.Message);
            }

            return host;
        }
    }
}
