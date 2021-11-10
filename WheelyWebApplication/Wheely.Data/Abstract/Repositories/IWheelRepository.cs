using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Abstract.Repositories
{
    public partial interface IWheelRepository : IEntityRepository<Wheel>
    {
        Wheel GetWheelWithRelatedTablesById(int id);
    }
}
