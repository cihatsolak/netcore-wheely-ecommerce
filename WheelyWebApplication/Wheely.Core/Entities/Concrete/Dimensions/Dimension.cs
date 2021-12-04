using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Dimensions
{
    public class Dimension : BaseEntity<int>
    {
        public int Size { get; set; }

        public virtual ICollection<Wheel> Wheels { get; set; }
    }
}
