
using AutoMapper;
using SSM.Models;
using SSM.ViewModels;

namespace SSM.DomainProfiles
{
    public class TradingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Country, CountryModel>();
            Mapper.CreateMap<Province, ProvinceModel>();
            Mapper.CreateMap<Supplier, SupplierModels>();
            Mapper.CreateMap<Warehouse, WareHouseModel>();
            Mapper.CreateMap<Product, ProductModel>()
              .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId ?? 0))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? src.Image.ToArray() : null));
            Mapper.CreateMap<ServicesType, ServicesViewModel>();
        }
    }
}