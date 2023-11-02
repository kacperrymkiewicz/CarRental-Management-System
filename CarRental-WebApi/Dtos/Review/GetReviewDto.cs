using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.User;

namespace CarRental_WebApi.Dtos.Review
{
    public class GetReviewDto
    {
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public GetUserDto Customer { get; set; }
        public GetCarDto Car { get; set; }
    }
}