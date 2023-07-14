using System.Text.Json.Serialization;

namespace learn_.net7.Models
{   
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        knight = 1,
        Mage = 2,
        Cloric = 3
    }
}