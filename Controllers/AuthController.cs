using Maksab.Dtos.Auth;
using Maksab.Helpers.MessageHandler;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IMessageHandler messageHandler) : base(messageHandler)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInputDto input)
        {
            return GetServiceResponse(await _authService.LoginAsync(input));
        }

    }
}
