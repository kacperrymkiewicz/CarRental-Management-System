using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Address;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Dtos.Rental
{
    public class GetRentalUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Customer;
        public GetAddressDto Address { get; set; }
    }
}