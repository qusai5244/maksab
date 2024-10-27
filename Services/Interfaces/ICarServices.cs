using Maksab.Dtos;
using Maksab.Dtos.Car;
using Maksab.Helpers;

namespace Maksab.Services.Interfaces
{
    public interface ICarServices
    {
        Task<ServiceResponse> AddCarAsync(AddNewCardDto input);
        Task<ServiceResponse<CarOutputDto>> GetCarAsync(int carId);
        Task<ServiceResponse<Pagination<CarListOutputDto>>> GetCarListAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateCarAsync(int carId, UpdateCardDto input);
    }
}
