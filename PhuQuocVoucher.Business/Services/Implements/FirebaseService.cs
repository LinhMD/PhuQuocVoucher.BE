using CrudApiTemplate.CustomException;
using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin.Auth;
using PhuQuocVoucher.Business.Services.Core;

namespace PhuQuocVoucher.Business.Services.Implements;

public class FirebaseService : IFirebaseServiceIntegration
{
    private static readonly string ApiKey = "AIzaSyDvD2uKXpme951jVJduE8l7g7SQGjSyqD4";
    private static readonly string Bucket = "phuquoc-client.appspot.com";
    private static readonly string AuthEmail = "adminuser@gmail.com";
    private static readonly string AuthPassword = "123456789";
    
    
    public async Task<string> UploadFileAsync(Stream file, string name)
    {
        var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        var authLink = auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword).Result;
        var task = new FirebaseStorage(Bucket, new FirebaseStorageOptions()
        {
            AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
            ThrowOnCancel = true,
        });
        try
        {
            return  await task.Child("file").Child(name).PutAsync(file);
        }
        catch (Exception e)
        {
            throw new ModelValueInvalidException(e.Message);
        }
    }

}