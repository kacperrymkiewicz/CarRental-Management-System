using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental_WebApi.Dtos.Car
{
    public class GetCarRatingDto
    {
        public double AverageRating { get; set; }
        public int ReviewsAmount { get; set; }
        public GetCarDto Car { get; set; }
    }
}