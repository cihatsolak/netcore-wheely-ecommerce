using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelCategoryConfiguration : IEntityTypeConfiguration<WheelCategory>
    {
        public void Configure(EntityTypeBuilder<WheelCategory> builder)
        {
            #region Table
            builder.ToTable(nameof(WheelCategory));
            builder.HasKey(p => new { p.WheelId, p.CategoryId });
            #endregion

            #region Relationships
            builder.HasOne(p => p.Wheel).WithMany(p => p.WheelCategories).HasForeignKey(p => p.WheelId);
            builder.HasOne(p => p.Category).WithMany(p => p.WheelCategories).HasForeignKey(p => p.CategoryId);
            #endregion
        }
    }
}
