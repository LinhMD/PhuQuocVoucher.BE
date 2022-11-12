using System.Text.Json.Serialization;

namespace PhuQuocVoucher.Data.Models;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BusyLevel
{
    Free,
    Moderate,
    Busy,
    Unavailable
}