using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Data.Abstract.Repositories.EntityFrameworkCore;
using Wheely.Data.Concrete.Contexts;
using Wheely.Data.Concrete.Extensions;

namespace Wheely.Data.Concrete.Repositories.EntityFrameworkCore
{
    public sealed class WheelRepository : EfEntityRepositoryBase<Wheel, WheelDbContext>, IWheelRepository
    {
        #region Constructor
        public WheelRepository(WheelDbContext wheelDbContext) : base(wheelDbContext)
        {
        }
        #endregion

        #region Methods
        public Wheel GetWheelWithRelatedTablesById(int id)
        {
            var wheel = Table.Includes("Comments", "Pictures", "Producer", "Categories", "Colors", "Dimensions", "Tags")
                        .AsSplitQuery()
                        .SingleOrDefault(p => p.Id == id);

            return wheel;
        }
        #endregion
    }
}
