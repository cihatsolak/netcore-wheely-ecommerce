using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Colors;

namespace Wheely.Data.Concrete.Configurations.Colors
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable(nameof(Color));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
            builder.Property(p => p.HexCode).HasMaxLength(15).IsRequired();
        }
    }
}
