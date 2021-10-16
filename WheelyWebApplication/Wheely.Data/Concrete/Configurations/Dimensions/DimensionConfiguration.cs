using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Dimensions;

namespace Wheely.Data.Concrete.Configurations.Dimensions
{
    public class DimensionConfiguration : IEntityTypeConfiguration<Dimension>
    {
        public void Configure(EntityTypeBuilder<Dimension> builder)
        {
            builder.ToTable(nameof(Dimension));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Size).HasDefaultValue<int>(0).IsRequired();
        }
    }
}
