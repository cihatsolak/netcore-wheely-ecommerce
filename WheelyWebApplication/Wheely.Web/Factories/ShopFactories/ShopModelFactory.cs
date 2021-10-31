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
            var wheelDetailViewModel = LazyAutoMapper.Wheel.Map<WheelDetailViewModel>(wheel);

            wheelDetailViewModel.Categories = wheel.Categories.Select(p => p.Name).ToList();
            wheelDetailViewModel.Tags = wheel.Tags.Select(p => p.Name).ToList();

            wheelDetailViewModel.WheelColorViewModels = wheel.Colors.Select(color => new WheelColorViewModel
            {
                Id = color.Id,
                HexCode = color.HexCode
            }).ToList();

            wheelDetailViewModel.WheelDimensionViewModels = wheel.Dimensions.Select(dimension => new WheelDimensionViewModel
            {
                Id = dimension.Id,
                Size = dimension.Size
            }).ToList();

            return wheelDetailViewModel;
        }
    }
}
