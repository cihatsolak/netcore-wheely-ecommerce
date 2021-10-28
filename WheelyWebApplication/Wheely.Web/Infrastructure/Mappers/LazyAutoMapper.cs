using AutoMapper;
using System;
using Wheely.Web.Infrastructure.Mappers.Profiles;

namespace Wheely.Web.Infrastructure.Mappers
{
    public static class LazyAutoMapper
    {
        private static readonly Lazy<IMapper> _wheelMapper = new(() =>
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<WheelMapperProfile>();
            });

            return mapperConfiguration.CreateMapper();
        });

        public static IMapper Wheel => _wheelMapper.Value;
    }
}
