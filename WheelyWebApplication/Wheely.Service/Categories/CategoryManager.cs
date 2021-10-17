using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Data.Concrete.Contexts;
using Wheely.Data.Concrete.EntityFrameworkCore;

namespace Wheely.Service.Categories
{
    public class CategoryManager : EfEntityRepositoryBase<Category>, ICategoryService
    {
        public CategoryManager(WheelDbContext wheelDbContext) : base(wheelDbContext)
        {
        }
    }
}
