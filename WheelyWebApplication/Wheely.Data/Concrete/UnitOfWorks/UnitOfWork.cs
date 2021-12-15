using System;
using Wheely.Data.Abstract.Repositories.EntityFrameworkCore;
using Wheely.Data.Abstract.Repositories.UnitOfWorks;
using Wheely.Data.Concrete.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Wheely.Data.Concrete.UnitOfWorks
{
    public sealed class UnitOfWork : BaseUnitOfWork<WheelDbContext>, IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(WheelDbContext context, IServiceProvider serviceProvider) : base(context)
        {
            _serviceProvider = serviceProvider;
        }

        public IWheelRepository Wheels => _serviceProvider.GetRequiredService<IWheelRepository>();

        public IRouteRepository Routes => _serviceProvider.GetRequiredService<IRouteRepository>();

        public ICategoryRepository Categories => _serviceProvider.GetRequiredService<ICategoryRepository>();
    }
}
