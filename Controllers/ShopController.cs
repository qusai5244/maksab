using Maksab.Dtos;
using Maksab.Dtos.Shop;
using Maksab.Helpers.MessageHandler;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : BaseController
    {
        public readonly IShopServices _shopServices;
        public ShopController(IShopServices shopServices, IMessageHandler messageHandler) : base(messageHandler)
        {
            _shopServices = shopServices;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateShopAsync([FromBody] CreateNewShopDto input)
        {
            return GetServiceResponse(await _shopServices.CreateShopAsync(input));
          
        }

        [HttpGet("")]
        public async Task<IActionResult> GetShopListAsync([FromQuery]GlobalFilterDto input)
        {
            return GetServiceResponse(await _shopServices.GetShopListAsync(input));
        }

        [HttpGet("{shopId}/{userId}")]
        public async Task<IActionResult> GetShopsAsync(int shopId,int userId)
        {
            return GetServiceResponse(await _shopServices.GetShopAsync(shopId,userId));
           
        }

        [HttpPut("{shopId}")]
        public async Task<IActionResult> UpdateShopAsync(int shopId, [FromBody] UpdateShopDto input)
        {
            return GetServiceResponse(await _shopServices.UpdateShopAsync(shopId, input));
           
        }


    }
}
