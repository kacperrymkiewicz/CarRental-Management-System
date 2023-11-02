using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Services.ReviewService
{
    public interface IReviewService
    {
        Task<ServiceResponse<GetReviewDto>> AddReview(AddReviewDto newReview);
        Task<ServiceResponse<GetReviewDto>> UpdateReview(UpdateReviewDto updatedReview);
        Task<ServiceResponse<bool>> DeleteReview(int id);
    }
}