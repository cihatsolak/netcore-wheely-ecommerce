using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Dimensions
{
    public class Dimension : BaseEntity
    {
        public int Size { get; set; }

        public virtual ICollection<WheelDimension> WheelDimensions { get; set; }
    }
}
