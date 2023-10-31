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
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
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

            var rentals = await _context.Rentals
                .Where(r => r.PickupDate < rentalTerm.ReturnDate && r.ReturnDate.AddHours(1) > rentalTerm.PickupDate && r.Status != RentalStatus.Cancelled)
                .ToListAsync();
            var cars = await _context.Cars.ToListAsync();
            rentals.ForEach(r => cars.Remove(r.Car));
            
            // var availableCars = await _context.Cars
            //     .Where(car => !_context.Rentals.All(r =>
            //         r.PickupDate < rentalTerm.ReturnDate && r.ReturnDate > rentalTerm.PickupDate && r.Status != RentalStatus.Cancelled))
            //     .ToListAsync();

            // var cars = await _context.Cars.
            //     Where(c => !_context.Rentals
            //         .Where(r => r.PickupDate < rentalTerm.ReturnDate && r.ReturnDate.AddHours(1) > rentalTerm.PickupDate && r.Status != RentalStatus.Cancelled)
            //         .Any(r => r.CarId == c.Id))
            //         .ToListAsync();

            serviceResponse.Data = cars.Select(c => _mapper.Map<GetCarDto>(c)).ToList();
            return serviceResponse;
        }
    }
}