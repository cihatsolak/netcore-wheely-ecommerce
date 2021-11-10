using System.Collections.Generic;
using Wheely.Core.Entities.Abstract;
using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Core.Entities.Concrete.Colors;
using Wheely.Core.Entities.Concrete.Comments;
using Wheely.Core.Entities.Concrete.Dimensions;
using Wheely.Core.Entities.Concrete.Pictures;
using Wheely.Core.Entities.Concrete.Producers;
using Wheely.Core.Entities.Concrete.Tags;

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


        #region One-To-Many Relationships
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        #endregion


        #region Many-To-Many Relationships
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Color> Colors { get; set; }
        public virtual ICollection<Dimension> Dimensions { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        #endregion
    }
}
