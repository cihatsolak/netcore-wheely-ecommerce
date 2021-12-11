using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelConfiguration : IEntityTypeConfiguration<Wheel>
    {
        public void Configure(EntityTypeBuilder<Wheel> builder)
        {
            #region Table
            builder.ToTable(nameof(Wheel));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.StarCount).IsRequired().HasDefaultValue<int>(0);
            builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
            builder.Property(p => p.ShortDescription).HasMaxLength(250).IsRequired();
            builder.Property(p => p.StockCode).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("numeric(18,2)");
            builder.Property(p => p.CampaignPrice).HasColumnType("numeric(18,2)");
            #endregion

            #region Indexes
            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Price);
            #endregion

            #region QueryFilters
            builder.HasQueryFilter(p => !p.IsDeleted);
            #endregion

            #region Relationships
            builder.HasMany(p => p.Categories).WithMany(p => p.Wheels);
            builder.HasMany(p => p.Colors).WithMany(p => p.Wheels);
            builder.HasMany(p => p.Dimensions).WithMany(p => p.Wheels);
            builder.HasMany(p => p.Tags).WithMany(p => p.Wheels);
            #endregion

            #region Optimized
            //builder.IsMemoryOptimized();
            #endregion
        }
    }
}
