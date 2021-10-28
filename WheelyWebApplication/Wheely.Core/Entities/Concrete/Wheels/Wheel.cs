using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Comments;
using Wheely.Core.Entities.Concrete.Pictures;
using Wheely.Core.Entities.Concrete.Producers;

namespace Wheely.Core.Entities.Concrete.Wheels
{
    public class Wheel : BaseEntity
    {
        public int StarCount { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string StockCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal CampaignPrice { get; set; }
        public bool IsDeleted { get; set; }

        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        public virtual ICollection<WheelCategory> WheelCategories { get; set; }
        public virtual ICollection<WheelColor> WheelColors { get; set; }
        public virtual ICollection<WheelDimension> WheelDimensions { get; set; }
        public virtual ICollection<WheelTag> WheelTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
