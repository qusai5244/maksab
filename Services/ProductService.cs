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

      
        public async Task<ServiceResponse> CreateProductAsync(CreateNewProductDto input)
        {
            try
            {
                // Check if the user exists and is active
                //var isUserExist = await _dataContext.Users
                //                                    .AsNoTracking()
                //                                    .AnyAsync(x => x.Id == input.UserId && !x.IsDeleted && x.IsActive);

                //if (!isUserExist)
                //{
                //    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "User not found");
                //}


                var product = new Product
                {
                    Name = input.Name,
                    NameAr = input.NameAr,
                    Status = input.Status,
                    Type = input.Type,
                    ShopId = input.ShopId,
                    ImagePAth = input.ImagePAth,
                };

                await _dataContext.Products.AddAsync(product);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Prodcut");
            }
            catch (Exception ex)
            {
                // will add logs later
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddProductAsync");
            }
        }

        public async Task<ServiceResponse> UpdateProductAsync(int Id, UpdateProductDto input)
        {
            try
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == Id);

                if (product == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Product");
                }

                product.Name = input.Name;
                product.NameAr = input.NameAr;
                product.Status = input.Status;
                product.Type = input.Type;
                product.ImagePAth = input.ImagePAth;
               

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Product");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateProductAsync");
            }
        }
        public async Task<ServiceResponse<GetProductDto>>GetProductAsync(int Id, int userId)
        {
            try
            {
                var product = await _dataContext
                                .Products
                                .AsNoTracking()
                                .Select(r => new GetProductDto
                                {
                                    Id = r.Id,
                                    Name = r.Name,
                                    NameAr = r.NameAr,
                                    ShopId = r.ShopId,
                                    UserId = r.UserId,
                                    Status = r.Status,
                                    Type = r.Type,
                                    ImagePAth = r.ImagePAth,
                                })//EXPLAIN
                                .FirstOrDefaultAsync(s => s.Id == Id && (s.UserId == userId));


                if (product == null)
                {
                    return _messageHandler.GetServiceResponse<GetProductDto>(ErrorMessage.NotFound, null, "Product");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, product);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<GetProductDto>(ErrorMessage.ServerInternalError, null, "GetProductAsync");
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

                // Step 3: Fetch the paginated list of Products using Skip and Take.
                var Products = await query
                                 .Skip(input.PageSize * (input.Page - 1))
                                 .Take(input.PageSize)
                                 .Select(x => new ProductListOutputDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     NameAr = x.NameAr,
                                     ShopId = x.ShopId,
                                     Status = x.Status,
                                     Type = x.Type,
                                     ImagePAth = x.ImagePAth,

                                 })
                                 .ToListAsync();

                // Step 4: Create a Pagination object with the retrieved items and total count.
                var paginationList = new Pagination<ProductListOutputDto>(Products, totalItems, input.Page, input.PageSize);

                // Step 5: Wrap the pagination result in a ServiceResponse and return it.
                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<Pagination<ProductListOutputDto>>(ErrorMessage.ServerInternalError, null, "AddProductAsync");
            }
        }

    }
}
