using Maksab.Dtos;
using Maksab.Dtos.Product;
using Maksab.Helpers;

namespace Maksab.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ServiceResponse> AddProductAsync(AddNewProductDto input);
        Task<ServiceResponse<ProductOutputDto>> GetProductAsync(int carId);
        Task<ServiceResponse<Pagination<ProductListOutputDto>>> GetProductListAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateProductAsync(int ProductId, UpdateProductDto input);
    }
}

