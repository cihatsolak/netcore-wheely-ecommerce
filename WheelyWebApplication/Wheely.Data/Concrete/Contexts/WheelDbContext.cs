using Microsoft.EntityFrameworkCore;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Data.Abstract.Contexts;

namespace Wheely.Data.Concrete.Contexts
{
    public class WheelDbContext : DbContext, IWheelDbContext
    {
        public WheelDbContext(DbContextOptions<WheelDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public DbSet<Wheel> Wheels { get; set; }
    }
}
