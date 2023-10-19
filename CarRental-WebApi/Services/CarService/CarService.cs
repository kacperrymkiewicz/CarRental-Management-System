using AutoMapper;
using CarRental_WebApi.Data;
using CarRental_WebApi.Dtos.Car;
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


        public async Task<ServiceResponse<List<GetCarDto>>> GetAllCars()
        {
            var serviceResponse = new ServiceResponse<List<GetCarDto>>();
            var cars = await _context.Cars.ToListAsync();
            serviceResponse.Data = cars.Select(c => _mapper.Map<GetCarDto>(c)).ToList();
            return serviceResponse;
        }

        public Task<ServiceResponse<GetCarDto>> GetCar(int id)
        {
            throw new NotImplementedException();
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

    }
}