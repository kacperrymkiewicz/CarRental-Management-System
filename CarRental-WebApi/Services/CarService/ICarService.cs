using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Services.CarService
{
    public interface ICarService
    {
        Task<ServiceResponse<GetCarDto>> GetCar(int id);
        Task<ServiceResponse<List<GetCarDto>>> GetAllCars();
    }
}