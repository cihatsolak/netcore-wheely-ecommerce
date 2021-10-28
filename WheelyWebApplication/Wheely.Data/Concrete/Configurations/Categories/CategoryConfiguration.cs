using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Categories;

namespace Wheely.Data.Concrete.Configurations.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Table
            builder.ToTable(nameof(Category));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(p => p.Name);
            #endregion
        }
    }
}
