using AutoMapper;
using ShoppingStore.OrderAPI.Models;
using ShoppingStore.OrderAPI.Models.DTO;

namespace ShoppingStore.OrderAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
