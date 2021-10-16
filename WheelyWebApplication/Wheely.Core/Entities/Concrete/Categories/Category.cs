using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<WheelCategory> WheelCategories { get; set; }
    }
}
