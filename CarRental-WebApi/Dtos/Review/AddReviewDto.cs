using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.User;

namespace CarRental_WebApi.Dtos.Review
{
    public class AddReviewDto
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
    }
}