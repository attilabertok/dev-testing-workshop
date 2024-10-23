using Microsoft.EntityFrameworkCore;

using TestingControllersSample.Core.Model;

namespace TestingControllersSample.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<BrainstormSession> BrainstormSessions { get; set; }
}