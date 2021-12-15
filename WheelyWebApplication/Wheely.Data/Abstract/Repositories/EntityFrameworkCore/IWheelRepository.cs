using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Abstract.Repositories.EntityFrameworkCore
{
    public partial interface IWheelRepository : IEntityRepository<Wheel, WheelDbContext>
    {
        Wheel GetWheelWithRelatedTablesById(int id);
    }
}
