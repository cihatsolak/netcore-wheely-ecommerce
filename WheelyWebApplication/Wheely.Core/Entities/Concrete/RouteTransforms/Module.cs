using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;

namespace Wheely.Core.Entities.Concrete.Routes
{
    public class Module : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<RouteValueTransform> RouteValueTransforms { get; set; }
    }
}
