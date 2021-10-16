using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Dimensions;

namespace Wheely.Core.Entities.Concrete.Wheels
{
    public class WheelDimension : IEntity
    {
        public int WheelId { get; set; }
        public Wheel Wheel { get; set; }

        public int DimensionId { get; set; }
        public Dimension Dimension { get; set; }
    }
}
