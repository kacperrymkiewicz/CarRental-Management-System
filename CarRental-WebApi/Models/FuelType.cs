using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FuelType
    {
        Diesel = 1,
        Gasoline = 2,
        LPG = 3,
        Eletric = 4,
        Hybrid = 5
    }
}