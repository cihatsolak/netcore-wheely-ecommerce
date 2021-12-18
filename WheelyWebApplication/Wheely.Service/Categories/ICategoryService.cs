namespace Wheely.Service.Categories
{
    public partial interface ICategoryService
    {
        IDataResult<List<Category>> GetCategoriesByCategoryIds(List<int> categoryIds);
    }
}
