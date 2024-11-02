using Maksab.Data;
using Maksab.Dtos;
using Maksab.Dtos.Shop;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Models;
using Maksab.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maksab.Services
{
    public class ShopService : IShopServices
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public ShopService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

      
        public async Task<ServiceResponse> CreateShopAsync(CreateNewShopDto input)
        {
            try
            {
                var shop = new Shop
                {
                    Name = input.Name,
                    NameAr = input.NameAr,
                    Status = input.Status,
                    Type = input.Type,
                    //UserId = input.UserId;
                    LogoPath = input.LogoPath,
                };

                await _dataContext.Shops.AddAsync(shop);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Shop");
            }
            catch (Exception ex)
            {
                // will add logs later
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddShopAsync");
            }
        }

        public async Task<ServiceResponse> UpdateShopAsync(int shopId, UpdateShopDto input)
        {
            try
            {
                var shop = await _dataContext.Shops.FirstOrDefaultAsync(x => x.ShopId == shopId);

                if (shop == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Shop");
                }

                shop.Name = input.Name;
                shop.NameAr = input.NameAr;
                shop.Status = input.Status;
                shop.Type = input.Type;
                //shop.UserId = input.UserId;
                shop.LogoPath = input.LogoPath;
               

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Shop");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateShopAsync");
            }
        }
        public async Task<ServiceResponse<GetShopDto>>GetShopsAsync(int shopId, int userId)
        {
            try
            {
                var shop = await _dataContext
                                .Shops
                                .AsNoTracking()
                                .Select(r => new GetShopDto
                                {
                                    ShopId = r.ShopId,
                                    Name = r.Name,
                                    NameAr = r.NameAr,
                                    UserId = r.UserId,
                                    Status = r.Status,
                                    Type = r.Type,
                                    LogoPath = r.LogoPath,
                                })//EXPLAIN
                                .FirstOrDefaultAsync(s => s.ShopId == shopId && (s.UserId == userId || userId == 0));


                if (shop == null)
                {
                    return _messageHandler.GetServiceResponse<GetShopDto>(ErrorMessage.NotFound, null, "Shop");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, shop);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<GetShopDto>(ErrorMessage.ServerInternalError, null, "GetShopAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<ShopListOutputDto>>> GetShopListAsync(GlobalFilterDto input)
        {
            try
            {
                // Step 1: Query without no db call yet to call it twice later one fro count and one for data.
                var query = _dataContext.Shops.AsNoTracking();

                // Step 2: Get the total item count for pagination.
                var totalItems = await query.CountAsync();

                // Step 3: Fetch the paginated list of Shops using Skip and Take.
                var Shops = await query
                                 .Skip(input.PageSize * (input.Page - 1))
                                 .Take(input.PageSize)
                                 .Select(x => new ShopListOutputDto
                                 {
                                     ShopId = x.ShopId,
                                     Name = x.Name,
                                     NameAr = x.NameAr,
                                     UserId = x.UserId,
                                     Status = x.Status,
                                     Type = x.Type,
                                     LogoPath = x.LogoPath,

                                 })
                                 .ToListAsync();

                // Step 4: Create a Pagination object with the retrieved items and total count.
                var paginationList = new Pagination<ShopListOutputDto>(Shops, totalItems, input.Page, input.PageSize);

                // Step 5: Wrap the pagination result in a ServiceResponse and return it.
                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<Pagination<ShopListOutputDto>>(ErrorMessage.ServerInternalError, null, "AddShopAsync");
            }
        }

    }
}
