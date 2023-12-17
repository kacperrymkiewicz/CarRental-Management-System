using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental_WebApi.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string EquipmentCode { get; set; }
        public string Description { get; set; }
        public List<Car> Cars { get; set; }
    }
}