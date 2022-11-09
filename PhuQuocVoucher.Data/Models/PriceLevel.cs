using System.Text.Json.Serialization;

namespace PhuQuocVoucher.Data.Models;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PriceLevel
{
   ChildrenCustomer,
   AdultCustomer,
   ChildrenSeller,
   AdultSeller,
   ChildrenProvider,
   AdultProvider,
   Combo,
   Default
}