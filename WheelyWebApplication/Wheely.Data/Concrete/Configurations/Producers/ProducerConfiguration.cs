using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Producers;

namespace Wheely.Data.Concrete.Configurations.Producers
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            #region Table
            builder.ToTable(nameof(Producer));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(p => p.Name);
            #endregion

            #region Relationships
            builder.HasMany(p => p.Wheels).WithOne(p => p.Producer).HasForeignKey(p => p.ProducerId);
            #endregion
        }
    }
}
