using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Pictures;

namespace Wheely.Data.Concrete.Configurations.Pictures
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            #region Table
            builder.ToTable(nameof(Picture));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.Url).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Order).IsRequired();
            #endregion

            #region Relationships
            builder.HasOne(p => p.Wheel).WithMany(p => p.Pictures).HasForeignKey(p => p.WheelId);
            #endregion
        }
    }
}
