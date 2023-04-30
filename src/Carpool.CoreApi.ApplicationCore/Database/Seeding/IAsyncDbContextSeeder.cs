using Microsoft.EntityFrameworkCore;

namespace Carpool.CoreApi.ApplicationCore.Database.Seeding
{
    public interface IAsyncDbContextSeeder<TContext> where TContext : DbContext
    {
        Task SeedAsync(TContext context, IServiceProvider provider);
    }
}
