using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Abstract.Repositories.EntityFrameworkCore
{
    public partial interface ICategoryRepository : IEntityRepository<Category, WheelDbContext>
    {
    }
}
