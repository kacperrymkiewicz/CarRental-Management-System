using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Dtos.User;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Dtos.Rental
{
    public class GetRentalDto
    {
        public DateTime ReservationDate { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public GetRentalUserDto Customer { get; set; }
        public GetCarDto Car { get; set; }
        public RentalStatus Status { get; set; }
    }
}