using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Categories;

namespace Wheely.Core.Entities.Concrete.Wheels
{
    public class WheelCategory : IEntity
    {
        public int WheelId { get; set; }
        public virtual Wheel Wheel { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
