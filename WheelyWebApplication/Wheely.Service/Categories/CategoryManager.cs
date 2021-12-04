using System.Collections.Generic;
using System.Linq;
using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Extensions;

namespace Wheely.Service.Categories
{
    public sealed class CategoryManager : ICategoryService
    {
        #region Fields
        private readonly ICategoryRepository _categoryRepository;
        #endregion

        #region Constructor
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        #region Methods
        public IDataResult<List<Category>> GetCategoriesByCategoryIds(List<int> categoryIds)
        {
            var categories = _categoryRepository.TableNoTracking.Where(category => categoryIds.Any(categoryId => categoryId == category.Id)).ToList();
            if (categories.IsNullOrNotAny())
                return new ErrorDataResult<List<Category>>();

            return new SuccessDataResult<List<Category>>(categories);
        }
        #endregion       
    }
}
