using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CarType
    {
        Sedan = 1,
        Coupe = 2,
        SportsCar = 3,
        Hatchback = 4,
        Suv = 5, 
        Compact = 6,
        Van = 7, 
        Pickup = 8
    }
}