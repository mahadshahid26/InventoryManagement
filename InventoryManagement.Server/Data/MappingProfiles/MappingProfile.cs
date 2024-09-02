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
            CreateMap<Sale, SaleDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
        }
    }
}
