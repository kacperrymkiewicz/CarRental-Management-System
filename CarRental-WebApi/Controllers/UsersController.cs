using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Rental;
using CarRental_WebApi.Dtos.User;
using CarRental_WebApi.Models;
using CarRental_WebApi.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(int id)
        {
            var response = await _userService.GetUser(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> UpdateUser(UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Delete(int id)
        {
            var response = await _userService.DeleteUser(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}/Rentals")]
        [Authorize(Roles = "Administrator, Employee, Customer")]
        public async Task<ActionResult<ServiceResponse<List<GetUserRentalsDto>>>> GetUserRentals(int id)
        {
            if(User.IsInRole("Customer")) {
                var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
                if(!userId.Equals(id)) {
                    return Unauthorized();
                }
            }

            var response = await _userService.GetUserRentals(id);

            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}