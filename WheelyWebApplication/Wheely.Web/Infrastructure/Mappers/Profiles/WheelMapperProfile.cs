using AutoMapper;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Web.Models.Shops;

namespace Wheely.Web.Infrastructure.Mappers.Profiles
{
    public class WheelMapperProfile : Profile
    {
        public WheelMapperProfile()
        {
            CreateMap<Wheel, WheelDetailViewModel>().ReverseMap();
        }
    }
}
