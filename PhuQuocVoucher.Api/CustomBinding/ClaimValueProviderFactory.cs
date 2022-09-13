using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhuQuocVoucher.Api.CustomBinding;

public class ClaimValueProviderFactory : IValueProviderFactory
{
    public static readonly BindingSource Claim = new(
        "Claim", // ID of our BindingSource, must be unique
        "BindingSource_Claim", // Display name
        isGreedy: false, // Marks whether the source is greedy or not
        isFromRequest: true); // Marks if the source is from HTTP Request
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        context.ValueProviders.Add(new ClaimValueProvider(Claim, context.ActionContext.HttpContext.User));
        return Task.CompletedTask;
    }
}