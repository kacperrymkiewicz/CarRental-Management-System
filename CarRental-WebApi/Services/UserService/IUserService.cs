using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Dtos.User;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> GetUser(int id);
        Task<ServiceResponse<List<GetUserDto>>> GetUsers();
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
        Task<ServiceResponse<List<GetUserRentalsDto>>> GetUserRentals(int id);
    }
}