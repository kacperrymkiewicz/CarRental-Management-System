using System.Text.Json.Serialization;

namespace CarRental_WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FuelType
    {
        Diesel,
        Gasoline,
        LPG,
        Eletric,
        Hybrid
    }
}