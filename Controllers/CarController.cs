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
    public class CarController : ControllerBase
    {
        public readonly ICarServices _carServices;
        private readonly IMessageHandler _messageHandler;

        public CarController(ICarServices carServices, IMessageHandler messageHandler)
        {
            _carServices = carServices;
            _messageHandler = messageHandler;

        }
        // Service Response
        public ActionResult GetServiceResponse<T>(ServiceResponse<T> response)
        {
            if (!response.Succeed)
            {
                return BadRequest(new ApiResponse(response.Code, response.Description, response.Result));
            }

            return Ok(new ApiResponse(response.Code, response.Description, response.Result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNewCardDto input)
        {
            var result = await _carServices.AddCarAsync(input);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _carServices.GetCarListAsync(input));
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> Get(int carId)
        {
            var result = await _carServices.GetCarAsync(carId);
            return Ok(result);
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> Put(int carId, [FromBody] UpdateCardDto input)
        {
            var result = await _carServices.UpdateCarAsync(carId, input);
            return Ok(result);
        }

    }
}
