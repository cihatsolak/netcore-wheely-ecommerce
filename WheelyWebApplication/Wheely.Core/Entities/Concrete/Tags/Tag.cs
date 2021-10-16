using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Tags
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<WheelTag> WheelTags { get; set; }
    }
}
