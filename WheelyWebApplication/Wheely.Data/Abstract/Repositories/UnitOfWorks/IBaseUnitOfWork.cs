using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Wheely.Data.Abstract.Repositories.UnitOfWorks
{
    public partial interface IBaseUnitOfWork<TContext> where TContext : DbContext 
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
