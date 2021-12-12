using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Abstract.Repositories.EntityFrameworkCore
{
    public partial interface IWheelRepository : IEntityRepository<Wheel>
    {
        Wheel GetWheelWithRelatedTablesById(int id);
    }
}
