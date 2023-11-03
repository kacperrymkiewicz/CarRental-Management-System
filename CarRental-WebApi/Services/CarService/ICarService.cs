using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Dtos.Reservation;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Services.CarService
{
    public interface ICarService
    {
        Task<ServiceResponse<GetCarDto>> GetCar(int id);
        Task<ServiceResponse<List<GetCarDto>>> GetCars();
        Task<ServiceResponse<List<GetCarDto>>> AddCar(AddCarDto newCar);
        Task<ServiceResponse<GetCarDto>> UpdateCar(UpdateCarDto updatedCar);
        Task<ServiceResponse<List<GetCarDto>>> DeleteCar(int id);
        Task<ServiceResponse<List<GetCarDto>>> GetAvailableCars(ReservationTermsDto rentalTerm);
        Task<ServiceResponse<bool>> CheckCarAvailability(int id, ReservationTermsDto rentalTerm);
        Task<ServiceResponse<GetCarRatingDto>> GetCarRating(int id);
    }
}