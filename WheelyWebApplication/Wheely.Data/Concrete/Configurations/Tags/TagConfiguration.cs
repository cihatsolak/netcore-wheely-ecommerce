using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Tags;

namespace Wheely.Data.Concrete.Configurations.Tags
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            #region Table
            builder.ToTable(nameof(Tag));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(p => p.Name).IsUnique(true);
            #endregion
        }
    }
}
