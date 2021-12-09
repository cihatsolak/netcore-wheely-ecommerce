using Microsoft.EntityFrameworkCore;
using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Core.Entities.Concrete.Colors;
using Wheely.Core.Entities.Concrete.Comments;
using Wheely.Core.Entities.Concrete.Dimensions;
using Wheely.Core.Entities.Concrete.Pictures;
using Wheely.Core.Entities.Concrete.Producers;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Core.Entities.Concrete.Tags;
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
            //modelBuilder.Entity<Wheel>().IsMemoryOptimized();
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public DbSet<Wheel> Wheels { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<RouteValueTransform> RouteValueTransforms { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
