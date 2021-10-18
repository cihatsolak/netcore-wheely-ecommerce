using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Wheels
{
    public partial interface IWheelService
    {
        IDataResult<Wheel> GetWheelById(int id);
    }
}
