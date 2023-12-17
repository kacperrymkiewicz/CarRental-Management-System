using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental_WebApi.Dtos.Car;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Dtos.Equipment
{
    public class GetEquipmentDto
    {
        public string EquipmentCode { get; set; }
        public string Description { get; set; }
    }
}