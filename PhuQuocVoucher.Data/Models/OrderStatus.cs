using System.Text.Json.Serialization;

namespace PhuQuocVoucher.Data.Models;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Processing,
    Confirm,
    Failed,
    Completed,
    Used,
    Canceled
}