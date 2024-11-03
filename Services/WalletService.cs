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


        public async Task<ServiceResponse> ActivateWalletAsync(int userId)
        {
            try
            {
                var wallet = await _dataContext.Wallets
                                                .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

                if (wallet == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Wallet");
                }

                if (!wallet.IsActive)
                {
                    wallet.IsActive = true;
                    wallet.UpdatedAt = DateTime.UtcNow;
                    await _dataContext.SaveChangesAsync();
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Wallet activated");
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                throw;
            }
        }
        public async Task<ServiceResponse> DeActivateWalletAsync(int userId)
        {
            try
            {
                var wallet = await _dataContext.Wallets
                                                .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

                if (wallet == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Wallet");
                }

                if (wallet.IsActive)
                {
                    wallet.IsActive = false;
                    wallet.UpdatedAt = DateTime.UtcNow;
                    await _dataContext.SaveChangesAsync();
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Wallet deactivated");
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                throw;
            }
        }
        public async Task<ServiceResponse> TopUpWalletAsync(int walletId, decimal amount)
        {
            try
            {
                var wallet = await _dataContext.Wallets
                                                .FirstOrDefaultAsync(x => x.Id == walletId && !x.IsDeleted);

                if (wallet == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Wallet");
                }

                var MaxBalance = 100;

                // Check if the new balance exceeds the maximum limit
                if (wallet.Balance + amount > MaxBalance)
                {
                    // change notFound to InvalidOperation
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, $"Cannot top up. Maximum balance is {MaxBalance}.");
                }

                // Update the wallet balance
                wallet.Balance += amount;
                wallet.UpdatedAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Wallet topped up successfully");
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                throw;
            }
        }
        public async Task<ServiceResponse> DebitWalletAsync(int walletId, decimal amount)
        {
            try
            {
                var wallet = await _dataContext.Wallets
                                                .FirstOrDefaultAsync(x => x.Id == walletId && !x.IsDeleted);

                if (wallet == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Wallet");
                }

                var MaxBalance = 0;

                // Check if the debit amount will result in a negative balance
                if (wallet.Balance - amount < MaxBalance)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, $"Cannot debit. Minimum balance is {MaxBalance}.");
                }

                // Update the wallet balance
                wallet.Balance -= amount;
                wallet.UpdatedAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Wallet debited successfully");
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
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
