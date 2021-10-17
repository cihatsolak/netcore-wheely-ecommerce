using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Pictures
{
    public class Picture : BaseEntity
    {
        public string Url { get; set; }
        public int Order { get; set; }

        public int WheelId { get; set; }
        public virtual Wheel Wheel { get; set; }
    }
}
