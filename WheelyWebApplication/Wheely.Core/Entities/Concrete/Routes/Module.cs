using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;

namespace Wheely.Core.Entities.Concrete.Routes
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
