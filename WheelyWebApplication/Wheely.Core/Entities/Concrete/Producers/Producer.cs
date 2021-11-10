using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Producers
{
    public class Producer : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Wheel> Wheels { get; set; }
    }
}
