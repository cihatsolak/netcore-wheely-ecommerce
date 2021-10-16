using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wheely.Core.Entities.Concrete.Comments;

namespace Wheely.Data.Concrete.Configurations.Comments
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FullName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Content).HasMaxLength(450).IsRequired();
            builder.Property(p => p.StarCount).HasDefaultValue<int>(0).IsRequired();
            builder.Property(p => p.ImageUrl).HasMaxLength(400).IsRequired();
            builder.Property(p => p.CreatedDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

            builder.HasOne(p => p.Wheel).WithMany(p => p.Comments).HasForeignKey(p => p.WheelId);
        }
    }
}
