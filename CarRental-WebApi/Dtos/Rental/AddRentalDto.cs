using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Dtos.Rental
{
    public class AddRentalDto
    {
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public RentalStatus Status { get; set; }
    }
}