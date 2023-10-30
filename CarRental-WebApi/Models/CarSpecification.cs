using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental_WebApi.Models
{
    public class CarSpecification
    {
        public string Image { get; set; }
        public string Gearbox { get; set; }
        public int DoorsNumber { get; set; }
        public int SeatsNumber { get; set; }
        public int TrunkCapacity { get; set; }
    }
}