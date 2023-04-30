using Carpool.CoreApi.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.CoreApi.ApplicationCore.Database;

public class CoreContext : BaseContext
{
    public CoreContext(DbContextOptions dbOptions)
        : base(dbOptions)
    { }

    protected CoreContext()
    { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Ride> Rides { get; set; }
    public DbSet<RidePlan> RidePlans { get; set; }
    public DbSet<RideEmployees> RideEmployees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
