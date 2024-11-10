using Maksab.Dtos.Auth;
using Maksab.Helpers.MessageHandler;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {

        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInputDto input)
        {
            return GetServiceResponse(await _authService.LoginAsync(input));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterInputDto input)
        {
            return GetServiceResponse(await _authService.RegisterAsync(input));
        }

    }
}
