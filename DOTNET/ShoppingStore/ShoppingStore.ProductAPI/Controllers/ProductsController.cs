using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStore.ProductAPI.Interfaces;
using ShoppingStore.ProductAPI.Models;
using ShoppingStore.ProductAPI.Models.DTO;

namespace ShoppingStore.ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private ResponseDTO _responseDTO;

        public ProductsController(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public ActionResult<ResponseDTO> Get()
        {
            try
            {
                var products = _productRepository.GetAll();
                var productsDto = _mapper.Map<IEnumerable<Product>>(products);
                _responseDTO.Result = productsDto;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ResponseDTO> Get(int id)
        {
            try
            {
                var product = _productRepository.Get(id);
                var productDto = _mapper.Map<Product>(product);
                _responseDTO.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpGet]
        [Route("getByName{name}")]
        public ActionResult<ResponseDTO> Get(string name)
        {
            try
            {
                var product = _productRepository.Get(p => p.Name.ToLower().Contains(name.ToLower()));
                var productDto = _mapper.Map<Product>(product);
                _responseDTO.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpPost]
        public ActionResult<ResponseDTO> Post([FromBody] ProductDTO productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                var result = _productRepository.Update(product);
                _responseDTO.Result = _mapper.Map<ProductDTO>(result);
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ResponseDTO> Delete(int id)
        {
            try
            {
                var product = _productRepository.Get(id);

                if (product != null)
                {
                    var response = _productRepository.Delete(product);
                    _responseDTO.Result = _mapper.Map<ProductDTO>(response);
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.ErrorMessage = ex.Message;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }
    }
}
