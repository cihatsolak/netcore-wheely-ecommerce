using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wheely.Core.Data;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Concrete.Repositories.EntityFrameworkCore
{
    public sealed class WheelRepository : EfEntityRepositoryBase<Wheel>, IWheelRepository
    {
        #region Constructor
        public WheelRepository(WheelDbContext wheelDbContext) : base(wheelDbContext)
        {
        }
        #endregion

        #region Methods
        public Wheel GetWheelWithRelatedTablesById(int id)
        {
            var wheel = TableNoTracking.Includes("Comments", "Pictures", "Producer")
                        .Include(p => p.WheelCategories).ThenInclude(p => p.Category)
                        .Include(p => p.WheelColors).ThenInclude(p => p.Color)
                        .Include(p => p.WheelDimensions).ThenInclude(p => p.Dimension)
                        .Include(p => p.WheelTags).ThenInclude(p => p.Tag)
                        .AsSplitQuery()
                        .SingleOrDefault(p => p.Id == id);

            return wheel;
        }
        #endregion
    }
}
