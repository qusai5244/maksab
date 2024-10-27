using Maksab.Dtos;
using Maksab.Dtos.Product;
using Maksab.Helpers;

namespace Maksab.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ServiceResponse> AddProductAsync(AddNewProductdDto input);
        Task<ServiceResponse<ProductOutputDto>> GetProductAsync(int carId);
        Task<ServiceResponse<Pagination<CarListOutputDto>>> GetProductListAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateProductAsync(int ProductId, UpdateProductDto input);
    }
}

