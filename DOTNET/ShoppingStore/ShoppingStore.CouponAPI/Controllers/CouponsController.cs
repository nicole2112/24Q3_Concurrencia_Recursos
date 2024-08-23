using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.CouponAPI.Data;
using ShoppingStore.CouponAPI.Models;
using ShoppingStore.CouponAPI.Models.DTO;

namespace ShoppingStore.CouponAPI.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDTO _responseDTO;
        public CouponsController(AppDbContext db, IMapper mapper) 
        {
            _db = db;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                var coupons = _db.Coupons.ToList();
                var couponsDto = _mapper.Map<IEnumerable<CouponDTO>>(coupons);
                _responseDTO.Result = couponsDto;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
            }
            return _responseDTO;
        }

        [HttpGet]
        [Route("{id}")]
        public object Get(int id)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(c => c.CouponId == id);
                var couponDto = _mapper.Map<CouponDTO>(coupon);
                _responseDTO.Result = couponDto;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
            }
            return _responseDTO;
        }

        [HttpPost]
        public ResponseDTO Post([FromBody]CouponDTO couponDTO)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();

                _responseDTO.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
            }
            return _responseDTO;
        }
    }
}
