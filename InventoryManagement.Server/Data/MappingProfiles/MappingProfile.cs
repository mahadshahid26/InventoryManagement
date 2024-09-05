using AutoMapper;
using InventoryManagement.Server.Data.Models;
using InventoryManagement.Server.Data.Models.ViewModels;
using InventoryManagement.Server.Models;

namespace InventoryManagement.Server.Data.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
            CreateMap<Purchase, PurchaseDTO>()
                .ForMember(dest=>dest.ProductName, opt=>opt.MapFrom(src=>src.Product.Name))
                .ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}
