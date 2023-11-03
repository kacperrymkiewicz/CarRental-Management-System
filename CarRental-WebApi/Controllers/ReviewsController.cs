using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Review;
using CarRental_WebApi.Models;
using CarRental_WebApi.Services.ReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService) 
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> GetReviews()
        {
            return Ok(await _reviewService.GetReviews());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetReviewDto>>> GetReview(int id)
        {
            var response = await _reviewService.GetReview(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> AddReview(AddReviewDto newReview)
        {
            return Ok(await _reviewService.AddReview(newReview));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> UpdateReview(UpdateReviewDto updatedReview)
        {
            var response = await _reviewService.UpdateReview(updatedReview);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ServiceResponse<List<GetReviewDto>>>> DeleteReview(int id)
        {
            var response = await _reviewService.DeleteReview(id);
            if(response.Data is null) 
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}