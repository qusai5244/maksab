using Maksab.Dtos;
using Maksab.Dtos.Product;
using Maksab.Helpers;
using Maksab.Models;

namespace Maksab.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ServiceResponse> CreateProductAsync(CreateNewProductDto input);
        Task<ServiceResponse> UpdateProductAsync(int Id, UpdateProductDto input);
        Task<ServiceResponse <GetProductDto>> GetProductAsync(int Id, int userId);
        Task<ServiceResponse<Pagination<ProductListOutputDto>>> GetProductListAsync(GlobalFilterDto input);
       
    }
}
