using Maksab.Data;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Models;
using Maksab.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maksab.Services
{
    public class WalletService : IWalletService
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;
        public WalletService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> CreateWalletAsync(int userId)
        {
            try
            {
                // validation
                var isUserExist = await _dataContext
                                .Users
                                .AsNoTracking()
                                .AnyAsync(x => x.Id == userId && !x.IsDeleted && x.IsActive);

                if (!isUserExist)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "user");
                }

                var isWalletExist = await _dataContext
                                          .Wallets
                                          .AsNoTracking()
                                          .AnyAsync(x => x.UserId == userId && !x.IsDeleted);

                if (isWalletExist)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.AlreadyExists, "Wallet");
                }

                var wallet = new Wallet
                {
                    UserId = userId,
                    Balance = 0,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false,
                };

                await _dataContext.Wallets.AddAsync(wallet);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Wallet");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        /*
         * new services
         * 1 - activated and decativate wallet 
         *      input -> userId -> from route
         * 
         * 2 - topup wallet 
         *     input -> walletId , amount -> from body
         *     rule : max balance = 100
         *     
         * 3- debit wallet
         *     input -> walletId , amount
         *     rule : min balance = 0
         *     
         */

    }
}
