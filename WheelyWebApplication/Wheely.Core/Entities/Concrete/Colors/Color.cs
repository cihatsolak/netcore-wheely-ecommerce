using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Colors
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public string HexCode { get; set; }

        public virtual ICollection<WheelColor> WheelColors { get; set; }
    }
}
