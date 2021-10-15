using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Concrete.Configurations.Wheels
{
    public class WheelConfiguration : IEntityTypeConfiguration<Wheel>
    {
        public void Configure(EntityTypeBuilder<Wheel> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
