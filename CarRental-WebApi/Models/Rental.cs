namespace CarRental_WebApi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public RentalStatus Status { get; set; }
    }
}