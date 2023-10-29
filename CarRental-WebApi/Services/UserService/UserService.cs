using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental_WebApi.Data;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Dtos.User;
using CarRental_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_WebApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public UserService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUser(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try 
            {
                var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);
                if (user is null)
                    throw new Exception($"Nie znaleziono użytkownika z ID: '{id}'");

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var users = await _context.Users.Include(u => u.Address).ToListAsync();
            serviceResponse.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
                if (user is null)
                    throw new Exception($"Nie znaleziono użytkownika z ID: '{updatedUser.Id}'");

                if (await _context.Users.AnyAsync(u => u.EmailAddress.ToLower() == updatedUser.EmailAddress.ToLower()))
                    throw new Exception($"Podany adres e-mail '{updatedUser.EmailAddress}' jest już zajęty");

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.EmailAddress = updatedUser.EmailAddress;
                user.AccountType = updatedUser.AccountType;
                user.Address = _mapper.Map<Address>(updatedUser.Address);

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user is null)
                    throw new Exception($"Nie znaleziono użytkownika z ID: '{id}'");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserRentalsDto>>> GetUserRentals(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserRentalsDto>>();
            try 
            {
                var user = await _context.Users
                    .Include(u => u.Rentals!).ThenInclude(r => r.Car)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (user is null)
                    throw new Exception($"Nie znaleziono użytkownika z ID: '{id}'");

                serviceResponse.Data = user.Rentals!.Select(r => _mapper.Map<GetUserRentalsDto>(r)).ToList();
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