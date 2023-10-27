using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Customer = 1,
        Employer = 2,
        Accountant = 3,
        Manager = 4,
        Administrator = 5
    }
}