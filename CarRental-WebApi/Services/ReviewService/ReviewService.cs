using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental_WebApi.Data;
using CarRental_WebApi.Dtos.Review;
using CarRental_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_WebApi.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetReviews()
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            var reviews = await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .ToListAsync();
            serviceResponse.Data = reviews.Select(r => _mapper.Map<GetReviewDto>(r)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDto>> GetReview(int id)
        {
            var serviceResponse = new ServiceResponse<GetReviewDto>();
            try 
            {
                var review = await _context.Reviews
                    .Include(r => r.Customer)
                    .Include(r => r.Car)
                    .FirstOrDefaultAsync(r => r.Id == id);
                    
                if (review is null)
                    throw new Exception($"Nie znaleziono opinii z ID: '{id}'");

                serviceResponse.Data = _mapper.Map<GetReviewDto>(review);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDto>> UpdateReview(UpdateReviewDto updatedReview)
        {
            var serviceResponse = new ServiceResponse<GetReviewDto>();

            try
            {
                var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == updatedReview.Id);
                if (review is null)
                    throw new Exception($"Nie znaleziono opinii z ID: '{updatedReview.Id}'");

                review.Rating = updatedReview.Rating;
                review.Date = updatedReview.Date;
                review.CustomerId = updatedReview.CustomerId;
                review.CarId = updatedReview.CarId;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetReviewDto>(review);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> AddReview(AddReviewDto newReview)
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();

            try
            {
                var customer = await _context.Users.AnyAsync(u => u.Id == newReview.CustomerId);
                if (!customer)
                    throw new Exception($"Nie znaleziono użytkownika z ID: '{newReview.CustomerId}'");

                var car = await _context.Cars.AnyAsync(c => c.Id == newReview.CarId);
                if (!car)
                    throw new Exception($"Nie znaleziono samochodu z ID: '{newReview.CarId}'");

                var review = _mapper.Map<Review>(newReview);
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Reviews.Select(r => _mapper.Map<GetReviewDto>(r)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> DeleteReview(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetReviewDto>>();

            try
            {
                var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
                if (review is null)
                    throw new Exception($"Nie znaleziono opinii z ID: '{id}'");

                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Reviews.Select(r => _mapper.Map<GetReviewDto>(r)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}