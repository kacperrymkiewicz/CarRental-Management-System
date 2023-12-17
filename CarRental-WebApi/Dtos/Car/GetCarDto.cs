using CarRental_WebApi.Dtos.Equipment;
using CarRental_WebApi.Models;

namespace CarRental_WebApi.Dtos.Car
{
    public class GetCarDto
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
        public decimal Price { get; set; }
        public string Image { get; set; }
        public GearboxType Gearbox { get; set; }
        public int DoorsNumber { get; set; }
        public int SeatsNumber { get; set; }
        public int TrunkCapacity { get; set; }
        public bool Status { get; set; } = true;
        public List<GetEquipmentDto> Equipment { get; set; }
    }
}