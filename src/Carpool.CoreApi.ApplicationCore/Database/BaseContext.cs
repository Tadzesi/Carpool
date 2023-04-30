using Microsoft.EntityFrameworkCore;

namespace Carpool.CoreApi.ApplicationCore.Database;

public abstract class BaseContext : DbContext
{
    protected BaseContext()
    { }

    protected BaseContext(DbContextOptions options) : base(options)
    { }


}
