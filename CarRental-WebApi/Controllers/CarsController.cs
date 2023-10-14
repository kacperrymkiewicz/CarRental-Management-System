using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Models;
using CarRental_WebApi.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService) 
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCarDto>>>> Get()
        {
            return Ok(await _carService.GetAllCars());
        }
    }
}