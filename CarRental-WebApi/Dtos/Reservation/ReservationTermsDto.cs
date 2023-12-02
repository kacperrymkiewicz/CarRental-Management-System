using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarRental_WebApi.Dtos.Reservation
{
    public class ReservationTermsDto
    {
        [BindRequired]
        public DateTime PickupDate { get; set; }
        [BindRequired]
        public DateTime ReturnDate { get; set; }
    }
}