using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelTagConfiguration : IEntityTypeConfiguration<WheelTag>
    {
        public void Configure(EntityTypeBuilder<WheelTag> builder)
        {
            builder.ToTable(nameof(WheelTag));
            builder.HasKey(p => new { p.TagId, p.WheelId });
            builder.HasOne(p => p.Tag).WithMany(p => p.WheelTags).HasForeignKey(p => p.TagId);
            builder.HasOne(p => p.Wheel).WithMany(p => p.WheelTags).HasForeignKey(p => p.WheelId);
        }
    }
}
