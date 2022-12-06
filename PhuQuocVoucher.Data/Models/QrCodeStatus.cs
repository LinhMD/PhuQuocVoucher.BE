using System.Text.Json.Serialization;

namespace PhuQuocVoucher.Data.Models;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QrCodeStatus
{
    Active, //sitting there doing nothing
    Pending,
    Commit, //set when order been confirmed by provider
    Used,   //set when qr code get scan
    Disable //set when get delete
} 