using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelColorConfiguration : IEntityTypeConfiguration<WheelColor>
    {
        public void Configure(EntityTypeBuilder<WheelColor> builder)
        {
            builder.ToTable(nameof(WheelColor));
            builder.HasKey(p => new { p.ColorId, p.WheelId });
            builder.HasOne(p => p.Color).WithMany(p => p.WheelColors).HasForeignKey(p => p.ColorId);
            builder.HasOne(p => p.Wheel).WithMany(p => p.WheelColors).HasForeignKey(p => p.WheelId);
        }
    }
}
