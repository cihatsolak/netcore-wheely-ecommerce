using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelDimensionConfiguration : IEntityTypeConfiguration<WheelDimension>
    {
        public void Configure(EntityTypeBuilder<WheelDimension> builder)
        {
            #region Table
            builder.ToTable(nameof(WheelDimension));
            builder.HasKey(p => new { p.DimensionId, p.WheelId });
            #endregion

            #region Relationships
            builder.HasOne(p => p.Dimension).WithMany(p => p.WheelDimensions).HasForeignKey(p => p.DimensionId);
            builder.HasOne(p => p.Wheel).WithMany(p => p.WheelDimensions).HasForeignKey(p => p.WheelId);
            #endregion
        }
    }
}
