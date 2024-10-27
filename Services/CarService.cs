using Maksab.Data;
using Maksab.Dtos;
using Maksab.Dtos.Car;
using Maksab.Helpers;
using Maksab.Helpers.MessageHandler;
using Maksab.Models;
using Maksab.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maksab.Services
{
    public class CarService : ICarServices
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;
        public CarService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddCarAsync(AddNewCardDto input)
        {
            try
            {
                var car = new Car
                {
                    Year = input.Year,
                    Mileage = input.Mileage,
                    Color = input.Color,
                    Price = input.Price,
                    FuelType = input.FuelType,
                    TransmissionType = input.TransmissionType,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dataContext.Cars.AddAsync(car);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Car");
            }
            catch (Exception ex)
            {
                // will add logs later
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddCarAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<CarListOutputDto>>> GetCarListAsync(GlobalFilterDto input)
        {
            try
            {
                // Step 1: Query without no db call yet to call it twice later one fro count and one for data.
                var query = _dataContext.Cars.AsNoTracking();

                // Step 2: Get the total item count for pagination.
                var totalItems = await query.CountAsync();

                // Step 3: Fetch the paginated list of cars using Skip and Take.
                var cars = await query
                                 .Skip(input.PageSize * (input.Page - 1))
                                 .Take(input.PageSize)
                                 .Select(x => new CarListOutputDto
                                 {
                                     Id = x.Id,
                                     Year = x.Year,
                                     Mileage = x.Mileage,
                                     Color = x.Color,
                                     Price = x.Price,
                                     FuelType = x.FuelType,
                                     TransmissionType = x.TransmissionType,
                                     CreatedAt = x.CreatedAt,
                                     UpdatedAt = x.UpdatedAt
                                 })
                                 .ToListAsync();

                // Step 4: Create a Pagination object with the retrieved items and total count.
                var paginationList = new Pagination<CarListOutputDto>(cars, totalItems, input.Page, input.PageSize);

                // Step 5: Wrap the pagination result in a ServiceResponse and return it.
                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<Pagination<CarListOutputDto>> (ErrorMessage.ServerInternalError,null, "AddCarAsync");
            }

        }

        public async Task<ServiceResponse<CarOutputDto>> GetCarAsync(int carId)
        {
            try
            {
                var car = await _dataContext
                                .Cars
                                .AsNoTracking()
                                .Select(r => new CarOutputDto
                                {
                                    Id = r.Id,
                                    Year = r.Year,
                                    Mileage = r.Mileage,
                                    Color = r.Color,
                                    Price = r.Price,
                                    FuelType = r.FuelType,
                                    TransmissionType = r.TransmissionType,
                                    CreatedAt = r.CreatedAt,
                                    UpdatedAt = r.UpdatedAt
                                })
                                .FirstOrDefaultAsync(x => x.Id == carId);

                if (car == null)
                {
                    return _messageHandler.GetServiceResponse<CarOutputDto>(ErrorMessage.NotFound, null, "Car");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, car);
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<CarOutputDto>(ErrorMessage.ServerInternalError,null, "GetCarAsync");
            }
        }

        public async Task<ServiceResponse> UpdateCarAsync(int carId, UpdateCardDto input)
        {
            try
            {
                var car = await _dataContext.Cars.FirstOrDefaultAsync(x => x.Id == carId);

                if (car == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Car");
                }

                car.Year = input.Year;
                car.Mileage = input.Mileage;
                car.Color = input.Color;
                car.Price = input.Price;
                car.FuelType = input.FuelType;
                car.TransmissionType = input.TransmissionType;
                car.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Car");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateCarAsync");
            }
        }


    }
}
