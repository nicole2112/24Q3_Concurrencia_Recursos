using AutoMapper;
using ShoppingStore.CouponAPI.Models;
using ShoppingStore.CouponAPI.Models.DTO;

namespace ShoppingStore.CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            //CreateMap<Coupon, CouponDTO>();
            //CreateMap<CouponDTO, Coupon>();

            CreateMap<Coupon, CouponDTO>().ReverseMap();
        }
    }
}
