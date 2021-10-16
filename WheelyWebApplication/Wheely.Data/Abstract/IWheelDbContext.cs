using Microsoft.EntityFrameworkCore;
using Wheely.Core.Entities.Concrete.Wheels;

namespace Wheely.Data.Abstract
{
    public interface IWheelDbContext
    {
        DbSet<Wheel> Wheels { get; set; }
    }
}
