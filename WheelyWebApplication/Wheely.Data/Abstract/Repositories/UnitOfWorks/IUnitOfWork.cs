using Wheely.Data.Abstract.Repositories.EntityFrameworkCore;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Abstract.Repositories.UnitOfWorks
{
    public partial interface IUnitOfWork : IBaseUnitOfWork<WheelDbContext>
    {
        IWheelRepository Wheels { get; }
        IRouteRepository Routes { get; }
        ICategoryRepository Categories { get; }
    }
}
