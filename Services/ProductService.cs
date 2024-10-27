using Maksab.Data;
using Maksab.Dtos;
using Maksab.Dtos.Product;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Models;
using Maksab.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maksab.Services
{
    public class ProductService : IProductServices
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;
        public ProductService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddProductAsync(AddNewProductDto input)
        {
            try
            {
                var product = new Product
                {
                    Id = input.Id,
                    Name = input.Name,
                    Description = input.Description,
                    Category = input.Category,
                    Price = input.Price,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dataContext.Products.AddAsync(product);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Product");
            }
            catch (Exception ex)
            {
                // will add logs later
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddProductAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<ProductListOutputDto>>> GetProductListAsync(GlobalFilterDto input)
        {
            try
            {
                // Step 1: Query without no db call yet to call it twice later one fro count and one for data.
                var query = _dataContext.Products.AsNoTracking();

                // Step 2: Get the total item count for pagination.
                var totalItems = await query.CountAsync();

                // Step 3: Fetch the paginated list of products using Skip and Take.
                var products = await query
                                 .Skip(input.PageSize * (input.Page - 1))
                                 .Take(input.PageSize)
                                 .Select(x => new ProductListOutputDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Description = x.Description,
                                     Category = x.Category,
                                     Price = x.Price,
                                     CreatedAt = x.CreatedAt,
                                     UpdatedAt = x.UpdatedAt
                                 })
                                 .ToListAsync();

                // Step 4: Create a Pagination object with the retrieved items and total count.
                var paginationList = new Pagination<ProductListOutputDto>(products, totalItems, input.Page, input.PageSize);

                // Step 5: Wrap the pagination result in a ServiceResponse and return it.
                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<Pagination<ProductListOutputDto>>(ErrorMessage.ServerInternalError, null, "AddProductAsync");
            }

        }

        public async Task<ServiceResponse<ProductOutputDto>> GetProductAsync(int productId)
        {
            try
            {
                var product = await _dataContext
                                .Products
                                .AsNoTracking()
                                .Select(r => new ProductOutputDto
                                {
                                    Id = r.Id,
                                    Name = r.Name,
                                    Description = r.Description,
                                    Category = r.Category,
                                    Price = r.Price,
                                    CreatedAt = r.CreatedAt,
                                    UpdatedAt = r.UpdatedAt
                                })
                                .FirstOrDefaultAsync(x => x.Id == productId);

                if (product == null)
                {
                    return _messageHandler.GetServiceResponse<ProductOutputDto>(ErrorMessage.NotFound, null, "Product");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, product);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<ProductOutputDto>(ErrorMessage.ServerInternalError, null, "GetProductAsync");
            }
        }

        public async Task<ServiceResponse> UpdateProductAsync(int productId, UpdateProductDto input)
        {
            try
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

                if (product == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Product");
                }

                product.Id = input.Id;
                product.Name = input.Name;
                product.Description = input.Description;
                product.Category = input.Category;
                product.Price = input.Price;
                product.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Product");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateProductAsync");
            }
        }


    }


}
