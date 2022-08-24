namespace PhuQuocVoucher.Api.Ultility;

public static class Common
{

    public const string SortByRegexString = @"(\w+-(asc|dec)(,|))*";

    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => "",
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static void Dump(this object? o)
    {
        Console.WriteLine(o);
    }
}