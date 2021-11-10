using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Web.Models.Shops;

namespace Wheely.Web.Factories.ShopFactories
{
    public partial interface IShopModelFactory
    {
        WheelDetailViewModel PrepareWheelDetailViewModel(Wheel wheel);
    }
}
