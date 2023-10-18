using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RentalStatus
    {
        Reserved = 1,
        Rented = 2,
        Returned = 3,
        Cancelled = 4
    }
}