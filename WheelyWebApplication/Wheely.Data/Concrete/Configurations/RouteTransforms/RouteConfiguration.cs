using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Routes;

namespace Wheely.Data.Concrete.Configurations.RouteTransforms
{
    public class RouteConfiguration : IEntityTypeConfiguration<RouteValueTransform>
    {
        public void Configure(EntityTypeBuilder<RouteValueTransform> builder)
        {
            #region Table
            builder.ToTable(nameof(RouteValueTransform));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.ControllerName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.ActionName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.SlugUrl).HasMaxLength(100).IsRequired();
            builder.Property(p => p.CustomUrl).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.EntityId).HasDefaultValue<int>(0).IsRequired();
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("now()").IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(p => p.SlugUrl).IsUnique();
            builder.HasIndex(p => p.CustomUrl).IsUnique();
            #endregion

            #region Relationships
            builder.HasOne(p => p.Module).WithMany(p => p.RouteValueTransforms).HasForeignKey(p => p.ModuleId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region QueryFilters
            builder.HasQueryFilter(p => !p.IsDeleted);
            #endregion
        }
    }
}
