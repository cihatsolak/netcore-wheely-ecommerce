using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Tags;

namespace Wheely.Core.Entities.Concrete.Wheels
{
    public class WheelTag : IEntity
    {
        public int WheelId { get; set; }
        public virtual Wheel Wheel { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
