using AutoMapper;
using CarRental_WebApi.Data;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.Reservation;
using CarRental_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_WebApi.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CarService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<GetCarDto>>> GetCars()
        {
            var serviceResponse = new ServiceResponse<List<GetCarDto>>();
            var cars = await _context.Cars.ToListAsync();
            serviceResponse.Data = cars.Select(c => _mapper.Map<GetCarDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCarDto>> GetCar(int id)
        {
            var serviceResponse = new ServiceResponse<GetCarDto>();
            try 
            {
                var car = await _context.Cars.Include(c => c.Equipment).FirstOrDefaultAsync(c => c.Id == id);
                if (car is null)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{id}'");

                serviceResponse.Data = _mapper.Map<GetCarDto>(car);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCarDto>>> AddCar(AddCarDto newCar)
        {
            var serviceResponse = new ServiceResponse<List<GetCarDto>>();
            var car = _mapper.Map<Car>(newCar);
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Cars.Select(c => _mapper.Map<GetCarDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCarDto>> UpdateCar(UpdateCarDto updatedCar)
        {
            var serviceResponse = new ServiceResponse<GetCarDto>();

            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == updatedCar.Id);
                if (car is null)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{updatedCar.Id}'");

                car.Brand = updatedCar.Brand;
                car.Model = updatedCar.Model;
                car.Type = updatedCar.Type;
                car.RegistrationPlate = updatedCar.RegistrationPlate;
                car.Engine = updatedCar.Engine;
                car.FuelType = updatedCar.FuelType;
                car.AvgFuelConsumption = updatedCar.AvgFuelConsumption;
                car.Horsepower = updatedCar.Horsepower;
                car.ProductionYear = updatedCar.ProductionYear;
                car.Price = updatedCar.Price;
                car.Status = updatedCar.Status;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCarDto>(car);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCarDto>>> DeleteCar(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCarDto>>();

            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
                if (car is null)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{id}'");

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Cars.Select(c => _mapper.Map<GetCarDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCarDto>>> GetAvailableCars(ReservationTermsDto rentalTerm)
        {
            var serviceResponse = new ServiceResponse<List<GetCarDto>>();
            int hourOffset = 1; // The time a car rental company needs for the inspection and preparation of the car for the next customer

            try
            {
                if (rentalTerm.PickupDate >= rentalTerm.ReturnDate)
                    throw new Exception("Data zwrotu nie może być wcześniejsza niż data odbioru");

                if (rentalTerm.PickupDate < DateTime.Now)
                    throw new Exception("Data odbioru nie może być starsza niż dzisiejsza data");

                var rentals = await _context.Rentals
                    .Where(r => r.PickupDate.AddHours(-hourOffset) < rentalTerm.ReturnDate && r.ReturnDate.AddHours(hourOffset) > rentalTerm.PickupDate && r.Status != RentalStatus.Cancelled)
                    .ToListAsync();
                var cars = await _context.Cars.Where(c => c.Status).ToListAsync();
                rentals.ForEach(r => cars.Remove(r.Car));

                serviceResponse.Data = cars.Select(c => _mapper.Map<GetCarDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> CheckCarAvailability(int id, ReservationTermsDto rentalTerm)
        {
            var serviceResponse = new ServiceResponse<bool>();
            
            try
            {
                if (rentalTerm.PickupDate >= rentalTerm.ReturnDate)
                    throw new Exception("Data zwrotu nie może być wcześniejsza niż data odbioru");

                if (rentalTerm.PickupDate < DateTime.Now)
                    throw new Exception("Data odbioru nie może być starsza niż dzisiejsza data");

                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
                if (car is null)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{id}'");

                var availableCars = await GetAvailableCars(rentalTerm);
                serviceResponse.Data = availableCars.Data!.Any(c => c.Id == id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCarRatingDto>> GetCarRating(int id)
        {
            var serviceResponse = new ServiceResponse<GetCarRatingDto>();

            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
                if (car is null)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{id}'");

                var reviews = await _context.Reviews.Where(r => r.CarId == id).ToListAsync();
                if (!reviews.Any())
                    throw new Exception($"Nie znaleziono opinii dla samochodu z ID: '{id}'");

                serviceResponse.Data = new GetCarRatingDto {
                    AverageRating = reviews.Average(r => r.Rating),
                    ReviewsAmount = reviews.Count,
                    Car = _mapper.Map<GetCarDto>(car)
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}