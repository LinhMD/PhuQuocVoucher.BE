using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhuQuocVoucher.Api.CustomBinding;

public class ClaimValueProvider: BindingSourceValueProvider
{
    private readonly ClaimsPrincipal _claimsPrincipal;

    public ClaimValueProvider(BindingSource bindingSource, ClaimsPrincipal claimsPrincipal) : base(bindingSource)
    {
        _claimsPrincipal = claimsPrincipal;
    }

    public override bool ContainsPrefix(string prefix)
        => _claimsPrincipal.HasClaim(claim => claim.Type == prefix);

    public override ValueProviderResult GetValue(string key)
    {
        var claimValue = _claimsPrincipal.FindFirstValue(key);
        return claimValue != null ? new ValueProviderResult(claimValue) : ValueProviderResult.None;
    }
}