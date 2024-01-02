using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Models;
using CarRental_WebApi.Services.RentalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService) 
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRentalDto>>>> GetRentals()
        {
            return Ok(await _rentalService.GetRentals());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRentalDto>>> GetRental(int id)
        {
            var response = await _rentalService.GetRental(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRentalDto>>>> AddRental(AddRentalDto newRental)
        {
            return Ok(await _rentalService.AddRental(newRental));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetRentalDto>>>> UpdateCar(UpdateRentalDto updatedRental)
        {
            var response = await _rentalService.UpdateRental(updatedRental);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ServiceResponse<List<GetRentalDto>>>> DeleteRental(int id)
        {
            var response = await _rentalService.DeleteRental(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPatch]
        [Route("{id}/Cancel")]
        public async Task<ActionResult<ServiceResponse<GetRentalDto>>> CancelRental(int id)
        {
            var response = await _rentalService.CancelRental(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPatch]
        [Route("{id}/Confirm")]
        [Authorize(Roles = "Administrator, Manager, Employee")]
        public async Task<ActionResult<ServiceResponse<GetRentalDto>>> ConfirmRental(int id)
        {
            var response = await _rentalService.ConfirmRental(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPatch]
        [Route("{id}/Finish")]
        [Authorize(Roles = "Administrator, Manager, Employee")]
        public async Task<ActionResult<ServiceResponse<GetRentalDto>>> FinishRental(int id)
        {
            var response = await _rentalService.FinishRental(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}