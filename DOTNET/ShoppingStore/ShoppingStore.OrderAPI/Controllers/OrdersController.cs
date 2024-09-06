using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.OrderAPI.Interfaces;
using ShoppingStore.OrderAPI.Models;
using ShoppingStore.OrderAPI.Models.DTO;

namespace ShoppingStore.OrderAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders() 
        { 
            var orders = _orderService.GetOrders();
            var ordersDto = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            var responseDto = new ResponseDTO<IEnumerable<OrderDTO>>
            {
                Result = ordersDto
            };
            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDTO orderDTO) // create order
        {
            var responseDto = new ResponseDTO<OrderDTO>();
            try
            {
                var order = _mapper.Map<Order>(orderDTO);
                var result = await _orderService.CreateOrderAsync(order);

                if (result != null)
                {
                    responseDto.Result = _mapper.Map<OrderDTO>(result);
                }
            }
            catch (Exception ex)
            {
                responseDto.Success = false;
                responseDto.ErrorMessage = ex.Message;
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("{orderId:int}")]
        public IActionResult Delete(int orderId) // cancel order
        {
            var responseDto = new ResponseDTO<OrderDTO>();
            try
            {
                var result = _orderService.CancelOrder(orderId);

                if (result != null)
                {
                    responseDto.Result = _mapper.Map<OrderDTO>(result);
                }
            }
            catch (Exception ex)
            {
                responseDto.Success = false;
                responseDto.ErrorMessage = ex.Message;
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
