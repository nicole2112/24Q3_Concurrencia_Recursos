using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.CouponAPI.Data;
using ShoppingStore.CouponAPI.Interfaces;
using ShoppingStore.CouponAPI.Models;
using ShoppingStore.CouponAPI.Models.DTO;

namespace ShoppingStore.CouponAPI.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IRepository<Coupon> _couponRepository;
        private readonly IMapper _mapper;
        private ResponseDTO _responseDTO;
        public CouponsController(IRepository<Coupon> repository, IMapper mapper) 
        {
            _couponRepository = repository;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                var coupons = _couponRepository.GetAll();
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
                var coupon = _couponRepository.Get(c => c.CouponId == id);
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

        [HttpGet]
        [Route("getByCode/{code}")]
        public object Get(string code)
        {
            try
            {
                var coupon = _couponRepository.Get(c => string.Equals(c.CouponCode.ToLower(), code.ToLower()));
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
        public ActionResult<ResponseDTO> Post([FromBody]CouponDTO couponDTO)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDTO);
                var result = _couponRepository.Add(coupon);

                _responseDTO.Result = _mapper.Map<CouponDTO>(result);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpPut]
        public ResponseDTO Put([FromBody]CouponDTO couponDTO)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDTO);
                var result = _couponRepository.Update(coupon);

                _responseDTO.Result = _mapper.Map<CouponDTO>(result);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
            }
            return _responseDTO;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var coupon = _couponRepository.Get(id);

                if (coupon != null)
                {
                    var result = _couponRepository.Delete(coupon);
                    _responseDTO.Result = _mapper.Map<CouponDTO>(result);
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest();
            }
            return Ok(_responseDTO);
        }
    }
}
