using System;
using Wheely.Core.Entities.Abstract;

namespace Wheely.Core.Entities.Concrete.Routes
{
    public class Route : BaseEntity
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string SlugUrl { get; set; }
        public string CustomUrl { get; set; }
        public int EntityId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}
