using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhuQuocVoucher.Api.CustomBinding;

//https://www.davidkaya.com/custom-from-attribute-for-controller-actions-in-asp-net-core/ - found it here
public class FromClaimAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
{
    public BindingSource BindingSource => ClaimValueProviderFactory.Claim;

    public FromClaimAttribute(string type)
    {
        Name = type;
    }

    public string Name { get; }
}