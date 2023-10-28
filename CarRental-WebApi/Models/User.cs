namespace CarRental_WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Customer;
        public Address Address { get; set; }
        public List<Rental>? Rentals { get; set; }
    }
}