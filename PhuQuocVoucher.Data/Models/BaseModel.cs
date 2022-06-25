using System.Text.Json.Serialization;

namespace PhuQuocVoucher.Data.Models;

public class BaseModel
{
    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }
    public ModelStatus Status { get; set; }

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ModelStatus
{
    Active,
    Delete
}