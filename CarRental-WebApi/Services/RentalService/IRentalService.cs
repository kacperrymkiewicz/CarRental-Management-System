using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Services.RentalService
{
    public interface IRentalService
    {
        Task<ServiceResponse<GetRentalDto>> GetRental(int id);
        Task<ServiceResponse<List<GetRentalDto>>> GetRentals();
        Task<ServiceResponse<List<GetRentalDto>>> AddRental(AddRentalDto newCar);
        Task<ServiceResponse<GetRentalDto>> UpdateRental(UpdateRentalDto updatedRental);
        Task<ServiceResponse<List<GetRentalDto>>> DeleteRental(int id);
        Task<ServiceResponse<GetRentalDto>> CancelRental(int id);
        Task<ServiceResponse<GetRentalDto>> ConfirmRental(int id);
        Task<ServiceResponse<GetRentalDto>> FinishRental(int id);
    }
}