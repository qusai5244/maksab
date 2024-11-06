using Maksab.Dtos;
using Maksab.Dtos.Shop;
using Maksab.Helpers;
using Maksab.Models;

namespace Maksab.Services.Interfaces
{
    public interface IShopServices
    {
        Task<ServiceResponse> CreateShopAsync(CreateNewShopDto input);
        Task<ServiceResponse> UpdateShopAsync(int shopId,UpdateShopDto input);
        Task<ServiceResponse <GetShopDto>>GetShopAsync(int shopId, int userId);
        Task<ServiceResponse<Pagination<ShopListOutputDto>>>GetShopListAsync(GlobalFilterDto input);
       
    }
}
