using System.ComponentModel.DataAnnotations;

namespace CarRental_WebApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}