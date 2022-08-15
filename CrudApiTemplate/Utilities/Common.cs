namespace PhuQuocVoucher.Api.Ultility;

public static class Common
{

    public const string SortByRegexString = @"(\w+-(asc|dec)(,|))*";

    public static void Dump(this object? o)
    {
        Console.WriteLine(o);
    }
}