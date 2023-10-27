using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Models;
using CarRental_WebApi.Services.CarService;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCarDto>>> GetSingle(int id)
        {
            return Ok(await _carService.GetCar(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCarDto>>>> AddCar(AddCarDto newCar)
        {
            return Ok(await _carService.AddCar(newCar));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCarDto>>>> UpdateCar(UpdateCarDto updatedCar)
        {
            var response = await _carService.UpdateCar(updatedCar);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ServiceResponse<List<GetCarDto>>>> DeleteCar(int id)
        {
            var response = await _carService.DeleteCar(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}