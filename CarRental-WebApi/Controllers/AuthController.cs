using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.UserAuthentication;
using CarRental_WebApi.Models;
using CarRental_WebApi.Repositories.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;

        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepository.Register(
                new User { FirstName = request.FirstName, LastName = request.LastName, EmailAddress = request.EmailAddress, Address = request.Address }, 
                request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.Login(request.EmailAddress, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePasswordDto request)
        {
            var response = await _authRepository.ChangePassword(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}