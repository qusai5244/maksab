using Maksab.Dtos;
using Maksab.Dtos.Product;
using Maksab.Helpers;

namespace Maksab.Services.Interfaces
{
    public interface ICarServices
    {
        Task<ServiceResponse> AddCarAsync(AddNewProductdDto input);
        Task<ServiceResponse<ProductOutputDto>> GetCarAsync(int carId);
        Task<ServiceResponse<Pagination<CarListOutputDto>>> GetCarListAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateCarAsync(int carId, UpdateCardDto input);
    }
}

