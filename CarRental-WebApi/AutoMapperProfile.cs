using AutoMapper;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Models;

namespace CarRental_WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, GetCarDto>();
        }
    }
}