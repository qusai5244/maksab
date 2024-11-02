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
        public async Task<IActionResult> Post([FromBody] CreateNewShopDto input)
        {
            var result = await _shopServices.CreateShopAsync(input);
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]GlobalFilterDto input)
        {
            return GetServiceResponse(await _shopServices.GetShopListAsync(input));
        }

        [HttpGet("{shopId}/{userId}")]
        public async Task<IActionResult> Get(int shopId,int userId)
        {
            var result = await _shopServices.GetShopsAsync(shopId,userId);
            return Ok(result);
        }

        [HttpPut("{shopId}")]
        public async Task<IActionResult> Put(int shopId, [FromBody] UpdateShopDto input)
        {
            var result = await _shopServices.UpdateShopAsync(shopId, input);
            return Ok(result);
        }


    }
}
