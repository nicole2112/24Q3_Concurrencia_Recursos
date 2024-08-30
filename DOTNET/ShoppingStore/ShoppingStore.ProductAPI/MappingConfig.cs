using AutoMapper;
using ShoppingStore.ProductAPI.Models;
using ShoppingStore.ProductAPI.Models.DTO;

namespace ShoppingStore.ProductAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            //CreateMap<Product, ProductDTO>();
            //CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
