using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhuQuocVoucher.Api.CustomBinding;

public static class ClaimBindingSource
{
    public static readonly BindingSource Claim = new(
        "Claim", // ID of our BindingSource, must be unique
        "BindingSource_Claim", // Display name
        isGreedy: false, // Marks whether the source is greedy or not
        isFromRequest: true); // Marks if the source is from HTTP Request
}