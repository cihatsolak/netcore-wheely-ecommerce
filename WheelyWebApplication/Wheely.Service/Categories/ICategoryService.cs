using System.Collections.Generic;
using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Categories
{
    public partial interface ICategoryService
    {
        IDataResult<List<Category>> GetCategoriesByCategoryIds(List<int> categoryIds);
    }
}
