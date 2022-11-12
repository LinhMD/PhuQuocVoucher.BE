using System.Text.Json.Serialization;

namespace PhuQuocVoucher.Data.Models;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Admin,
    Seller,
    Provider,
    Customer
}