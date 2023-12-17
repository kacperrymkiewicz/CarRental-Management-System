using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental_WebApi.Data;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Dtos.Reservation;
using CarRental_WebApi.Models;
using CarRental_WebApi.Services.CarService;
using Microsoft.EntityFrameworkCore;

namespace CarRental_WebApi.Services.RentalService
{
    public class RentalService : IRentalService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICarService _carService;
        
        public RentalService(IMapper mapper, DataContext context, ICarService carService)
        {
            _context = context;
            _mapper = mapper;
            _carService = carService;
        }

        public async Task<ServiceResponse<List<GetRentalDto>>> GetRentals()
        {
            var serviceResponse = new ServiceResponse<List<GetRentalDto>>();
            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .ToListAsync();
            serviceResponse.Data = rentals.Select(r => _mapper.Map<GetRentalDto>(r)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRentalDto>> GetRental(int id)
        {
            var serviceResponse = new ServiceResponse<GetRentalDto>();
            try 
            {
                var rental = await _context.Rentals
                    .Include(r => r.Car)
                    .Include(r => r.Customer)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (rental is null)
                    throw new Exception($"Nie znaleziono wynajmu z ID: '{id}'");

                serviceResponse.Data = _mapper.Map<GetRentalDto>(rental);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRentalDto>>> AddRental(AddRentalDto newRental)
        {
            var serviceResponse = new ServiceResponse<List<GetRentalDto>>();
            var rental = _mapper.Map<Rental>(newRental);

            try
            {
                if (rental.PickupDate >= rental.ReturnDate)
                    throw new Exception("Data zwrotu nie może być wcześniejsza niż data odbioru");

                var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == rental.CustomerId);
                if (customer is null)
                    throw new Exception($"Nie znaleziono klienta z ID: '{rental.CustomerId}'");

                var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == rental.CarId);
                if (car is null)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{rental.CarId}'");

                var available = await _carService.CheckCarAvailability(car.Id, new ReservationTermsDto { PickupDate = rental.PickupDate, ReturnDate = rental.ReturnDate});
                if (!available.Data)
                    throw new Exception($"Samochód z ID: '{rental.CarId}' nie jest dostępny w terminie od '{rental.PickupDate}' do '{rental.ReturnDate}'");

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Rentals.Select(r => _mapper.Map<GetRentalDto>(r)).ToListAsync();  
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRentalDto>> UpdateRental(UpdateRentalDto updatedRental)
        {
            var serviceResponse = new ServiceResponse<GetRentalDto>();

            try
            {
                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == updatedRental.Id);
                if (rental is null)
                    throw new Exception($"Nie znaleziono wynajmu z ID: '{updatedRental.Id}'");

                rental.PickupDate = updatedRental.PickupDate;
                rental.ReturnDate = updatedRental.ReturnDate;
                rental.CustomerId = updatedRental.CustomerId;
                rental.CarId = updatedRental.CarId;
                rental.Status = updatedRental.Status;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRentalDto>(rental);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRentalDto>>> DeleteRental(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetRentalDto>>();

            try
            {
                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id);
                if (rental is null)
                    throw new Exception($"Nie znaleziono wynajmu z ID: '{id}'");

                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Rentals.Select(r => _mapper.Map<GetRentalDto>(r)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRentalDto>> CancelRental(int id)
        {
            var serviceResponse = new ServiceResponse<GetRentalDto>();

            try
            {
                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id);
                if(rental is null)
                    throw new Exception($"Nie znaleziono rezerwacji z ID: '{id}'");

                if((rental.PickupDate - DateTime.Now) <= TimeSpan.FromHours(24))
                    throw new Exception($"Nie można odwołać rezerwacji, ponieważ pozostało mniej niż 24 godziny");

                rental.Status = RentalStatus.Cancelled;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRentalDto>(rental);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRentalDto>> ConfirmRental(int id)
        {
            var serviceResponse = new ServiceResponse<GetRentalDto>();

            try
            {
                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id);
                if(rental is null)
                    throw new Exception($"Nie znaleziono rezerwacji z ID: '{id}'");

                rental.Status = RentalStatus.Reserved;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRentalDto>(rental);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRentalDto>> FinishRental(int id)
        {
            var serviceResponse = new ServiceResponse<GetRentalDto>();

            try
            {
                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id);
                if(rental is null)
                    throw new Exception($"Nie znaleziono rezerwacji z ID: '{id}'");

                rental.Status = RentalStatus.Returned;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRentalDto>(rental);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}