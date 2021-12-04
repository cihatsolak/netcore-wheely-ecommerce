using System;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Core.Entities.Concrete.Comments
{
    public class Comment : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string Content { get; set; }
        public  int StarCount { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }

        public int WheelId { get; set; }
        public virtual Wheel Wheel { get; set; }
    }
}
