using Maksab.Dtos.Auth;
using Maksab.Helpers.MessageHandler;
using Maksab.Services;
using Maksab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : BaseController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService, IMessageHandler messageHandler) : base(messageHandler)
        {
            _walletService = walletService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> LoginAsync([FromRoute]int userId)
        {
            return GetServiceResponse(await _walletService.CreateWalletAsync(userId));
        }
    }
}
