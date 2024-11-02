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

        [HttpPut("{userId}/activate")]
        public async Task<IActionResult> ActivateWalletAsync([FromRoute] int userId)
        {
            return GetServiceResponse(await _walletService.ActivateWalletAsync(userId));
        }
        // Deactivate Wallet
        [HttpPut("{userId}/deactivate")]
        public async Task<IActionResult> DeactivateWalletAsync([FromRoute] int userId)
        {
            return GetServiceResponse(await _walletService.DeactivateWalletAsync(userId));
        }

        // Top Up Wallet
        [HttpPost("{walletId}/topup")]
        public async Task<IActionResult> TopUpWalletAsync([FromRoute] int walletId, [FromBody] decimal amount)
        {
            return GetServiceResponse(await _walletService.TopUpWalletAsync(walletId, amount));
        }

        // Debit Wallet
        [HttpPost("{walletId}/debit")]
        public async Task<IActionResult> DebitWalletAsync([FromRoute] int walletId, [FromBody] decimal amount)
        {
            return GetServiceResponse(await _walletService.DebitWalletAsync(walletId, amount));
        }


    }
}
