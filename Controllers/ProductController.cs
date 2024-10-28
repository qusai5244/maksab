using Maksab.Dtos;
using Maksab.Dtos.Product;
using Maksab.Helpers.MessageHandler;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        public readonly IProductServices _productServices;
        public ProductController(IProductServices productServices, IMessageHandler messageHandler) : base(messageHandler)
        {
            _productServices = productServices;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] AddNewProductDto input)
        {
            var result = await _productServices.AddProductAsync(input);
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _productServices.GetProductListAsync(input));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int carId)
        {
            var result = await _productServices.GetProductAsync(carId);
            return Ok(result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Put(int productId, [FromBody] UpdateProductDto input)
        {
            var result = await _productServices.UpdateProductAsync(productId, input);
            return Ok(result);
        }


    }
}
