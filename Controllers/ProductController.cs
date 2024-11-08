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
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateNewProductDto input)
        {
            return GetServiceResponse(await _productServices.CreateProductAsync(input));
          
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProductListAsync([FromQuery]GlobalFilterDto input)
        {
            return GetServiceResponse(await _productServices.GetProductListAsync(input));
        }

        [HttpGet("{Id}/{userId}")]
        public async Task<IActionResult> GetProductsAsync(int Id,int userId)
        {
            return GetServiceResponse(await _productServices.GetProductAsync(Id,userId));
           
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateProductAsync(int Id, [FromBody] UpdateProductDto input)
        {
            return GetServiceResponse(await _productServices.UpdateProductAsync(Id, input));
           
        }


    }
}
