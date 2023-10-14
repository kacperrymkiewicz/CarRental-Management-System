namespace CarRental_WebApi.Dtos.Car
{
    public class GetCarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string RegistrationPlate { get; set; }
        public int ProductionYear { get; set; }
        public decimal Price { get; set; }
    }
}