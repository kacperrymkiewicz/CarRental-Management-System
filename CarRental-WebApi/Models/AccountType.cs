using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Customer,
        Employee,
        Accountant,
        Manager,
        Administrator
    }
}