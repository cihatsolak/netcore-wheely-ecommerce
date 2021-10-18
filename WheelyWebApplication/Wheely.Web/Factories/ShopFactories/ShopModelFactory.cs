using System.Linq;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Web.Infrastructure.Mappers;
using Wheely.Web.Models.Shops;

namespace Wheely.Web.Factories.ShopFactories
{
    public sealed class ShopModelFactory : IShopModelFactory
    {
        public WheelDetailViewModel PrepareWheelDetailViewModel(Wheel wheel)
        {
            var wheelDetailViewModel = LazyAutoMapper.Mapper.Map<WheelDetailViewModel>(wheel);

            wheelDetailViewModel.Categories = wheel.WheelCategories.Select(p => p.Category.Name).ToList();
            wheelDetailViewModel.Tags = wheel.WheelTags.Select(p => p.Tag.Name).ToList();

            wheelDetailViewModel.WheelColorViewModels = wheel.WheelColors.Select(item => new WheelColorViewModel
            {
                Id = item.Color.Id,
                HexCode = item.Color.HexCode
            }).ToList();

            wheelDetailViewModel.WheelDimensionViewModels = wheel.WheelDimensions.Select(item => new WheelDimensionViewModel
            {
                Id = item.Dimension.Id,
                Size = item.Dimension.Size
            }).ToList();

            return wheelDetailViewModel;
        }
    }
}
