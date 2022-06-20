namespace PhuQuocVoucher.Api.Ultility;

public static class Common
{
    public static readonly string ApiV1Prefix = "api/v1";

    public static void Dump(this object? o)
    {
        Console.WriteLine(o);
    }
}