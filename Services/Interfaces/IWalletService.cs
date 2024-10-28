using Maksab.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Maksab.Services.Interfaces
{

    public interface IWalletService
    {
        Task<ServiceResponse> CreateWalletAsync(int userId);
    }
}
