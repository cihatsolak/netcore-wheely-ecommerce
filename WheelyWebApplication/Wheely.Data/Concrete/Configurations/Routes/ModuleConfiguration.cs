using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wheely.Core.Entities.Concrete.Routes;

namespace Wheely.Data.Concrete.Configurations.Routes
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            #region Table
            builder.ToTable(nameof(Module));
            builder.HasKey(p => p.Id);
            #endregion

            #region Properties
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            #endregion

            #region Indexes
            builder.HasIndex(p => p.Name).IsUnique();
            #endregion
        }
    }
}
