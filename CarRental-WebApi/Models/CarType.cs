using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CarType
    {
        Sedan,
        Coupe,
        SportsCar,
        Hatchback,
        Suv, 
        Compact,
        Van, 
        Pickup
    }
}