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
