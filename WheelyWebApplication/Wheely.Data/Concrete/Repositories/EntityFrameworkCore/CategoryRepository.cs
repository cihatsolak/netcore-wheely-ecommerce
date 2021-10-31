using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Concrete.Repositories.EntityFrameworkCore
{
    public sealed class CategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        #region Constructor
        public CategoryRepository(WheelDbContext wheelDbContext) : base(wheelDbContext)
        {
        }
        #endregion
    }
}
