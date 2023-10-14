namespace CarRental_WebApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
    }
}