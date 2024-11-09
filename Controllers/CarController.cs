using Maksab.Dtos;
using Maksab.Dtos.Car;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController(ICarServices carServices, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly ICarServices _carServices = carServices;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNewCardDto input)
        {
            return GetServiceResponse(await _carServices.AddCarAsync(input));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _carServices.GetCarListAsync(input));
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> Get(int carId)
        {
            return GetServiceResponse(await _carServices.GetCarAsync(carId));
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> Put(int carId, [FromBody] UpdateCardDto input)
        {
            return GetServiceResponse(await _carServices.UpdateCarAsync(carId, input));
        }

    }
}
