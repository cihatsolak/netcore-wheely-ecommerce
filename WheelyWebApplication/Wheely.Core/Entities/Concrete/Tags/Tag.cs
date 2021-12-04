using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Tags
{
    public class Tag : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Wheel> Wheels { get; set; }
    }
}
