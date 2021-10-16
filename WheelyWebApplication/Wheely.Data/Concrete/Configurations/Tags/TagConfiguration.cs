using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Tags;

namespace Wheely.Data.Concrete.Configurations.Tags
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

            builder.HasIndex(p => p.Name).IsUnique(true);
        }
    }
}
