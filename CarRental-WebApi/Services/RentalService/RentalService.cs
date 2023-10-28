using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental_WebApi.Data;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_WebApi.Services.RentalService
{
    public class RentalService : IRentalService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public RentalService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetRentalDto>>> GetRentals()
        {
            var serviceResponse = new ServiceResponse<List<GetRentalDto>>();
            var rentals = await _context.Rentals.ToListAsync();
            serviceResponse.Data = rentals.Select(r => _mapper.Map<GetRentalDto>(r)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRentalDto>> GetRental(int id)
        {
            var serviceResponse = new ServiceResponse<GetRentalDto>();
            try 
            {
                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Id == id);
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
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Rentals.Select(r => _mapper.Map<GetRentalDto>(r)).ToListAsync();
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
    }
}