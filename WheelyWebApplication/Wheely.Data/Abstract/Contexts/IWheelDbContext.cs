using Microsoft.EntityFrameworkCore;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Abstract.Contexts
{
    public partial interface IWheelDbContext
    {
        DbSet<Wheel> Wheels { get; set; }
    }
}
