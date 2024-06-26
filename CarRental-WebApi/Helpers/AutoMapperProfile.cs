using AutoMapper;
using CarRental_WebApi.Dtos.Address;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.Equipment;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Dtos.Review;
using CarRental_WebApi.Dtos.User;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, GetCarDto>();
            CreateMap<GetCarDto, Car>();
            CreateMap<AddCarDto, Car>();
            CreateMap<Rental, GetRentalDto>();
            CreateMap<Rental, GetUserRentalsDto>();
            CreateMap<AddRentalDto, Rental>();
            CreateMap<GetUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<User, GetRentalUserDto>();
            CreateMap<GetAddressDto, Address>();
            CreateMap<Address, GetAddressDto>();
            CreateMap<Review, GetReviewDto>();
            CreateMap<GetReviewDto, Review>();
            CreateMap<AddReviewDto, Review>();
            CreateMap<Equipment, GetEquipmentDto>();
        }
    }
}