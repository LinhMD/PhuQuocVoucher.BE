namespace PhuQuocVoucher.Business.Services.Core;

public interface IFirebaseServiceIntegration
{
    public Task<string> UploadFileAsync(Stream file, string name);

}