using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelConfiguration : IEntityTypeConfiguration<Wheel>
    {
        public void Configure(EntityTypeBuilder<Wheel> builder)
        {
            builder.ToTable(nameof(Wheel));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.StarCount).IsRequired().HasDefaultValue<int>(0);
            builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
            builder.Property(p => p.ShortDescription).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("numeric(18,2)");
            builder.Property(p => p.CampaignPrice).HasColumnType("numeric(18,2)");
        }
    }
}
