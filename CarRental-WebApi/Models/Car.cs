using Microsoft.EntityFrameworkCore;

namespace CarRental_WebApi.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public CarType Type { get; set; }
        public string RegistrationPlate { get; set; }
        public string Engine { get; set; }
        public FuelType FuelType { get; set; }
        public float AvgFuelConsumption { get; set; }
        public int Horsepower { get; set; }
        public int ProductionYear { get; set; }
        [Precision(6,2)]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public GearboxType Gearbox { get; set; }
        public int DoorsNumber { get; set; }
        public int SeatsNumber { get; set; }
        public int TrunkCapacity { get; set; }
        public bool Status { get; set; } = true;
    }
}