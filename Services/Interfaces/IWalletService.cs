using Maksab.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Services.Interfaces
{

    public interface IWalletService
    {
        Task<ServiceResponse> ActivateWalletAsync(int userId);
        Task<ServiceResponse> CreateWalletAsync(int userId);
        Task<ServiceResponse> DeactivateWalletAsync(int userId);
        Task<ServiceResponse> DebitWalletAsync(int walletId, decimal amount);
        Task<ServiceResponse> TopUpWalletAsync(int walletId, decimal amount);
    }
}
