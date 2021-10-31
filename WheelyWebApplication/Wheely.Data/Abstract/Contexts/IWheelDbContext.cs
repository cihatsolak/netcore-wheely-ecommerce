using Microsoft.EntityFrameworkCore;
using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Core.Entities.Concrete.Colors;
using Wheely.Core.Entities.Concrete.Comments;
using Wheely.Core.Entities.Concrete.Dimensions;
using Wheely.Core.Entities.Concrete.Pictures;
using Wheely.Core.Entities.Concrete.Producers;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Core.Entities.Concrete.Tags;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Abstract.Contexts
{
    public partial interface IWheelDbContext
    {
        DbSet<Wheel> Wheels { get; set; }
        DbSet<Module> Modules { get; set; }
        DbSet<Route> Routes { get; set; }
        DbSet<Producer> Producers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<Dimension> Dimensions { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Picture> Pictures { get; set; }
    }
}
