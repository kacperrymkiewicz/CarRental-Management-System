using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.User;

namespace CarRental_WebApi.Dtos.Review
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public GetUserDto Customer { get; set; }
        public GetCarDto Car { get; set; }
    }
}