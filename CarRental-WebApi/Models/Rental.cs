namespace CarRental_WebApi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public bool Status { get; set; }
    }
}