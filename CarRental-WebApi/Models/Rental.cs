namespace CarRental_WebApi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User Customer { get; set; }
        public Car Car { get; set; }
        public RentalStatus Status { get; set; }
    }
}