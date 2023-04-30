using Microsoft.EntityFrameworkCore;

namespace Carpool.CoreApi.ApplicationCore.Database.Seeding
{
    public interface IDbContextSeeder<TContext> where TContext : DbContext
    {
        void Seed(TContext context, IServiceProvider provider);
    }
}
