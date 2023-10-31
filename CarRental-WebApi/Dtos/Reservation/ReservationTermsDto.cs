using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental_WebApi.Dtos.Reservation
{
    public class ReservationTermsDto
    {
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}