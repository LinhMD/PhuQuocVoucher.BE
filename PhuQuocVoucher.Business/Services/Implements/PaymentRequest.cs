using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace PhuQuocVoucher.Business.Services.Implements;

public static class PaymentRequest
{
    public static async Task<string> SendPaymentRequestAsync(string endpoint, string postJsonString)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization",
                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("momo:momo")));
            var result = await client.PostAsync(endpoint, new StringContent(postJsonString, Encoding.UTF8, "application/json"));
            var resultString = "";


            using var reader = new StreamReader(await result.Content.ReadAsStreamAsync());
            resultString = await reader.ReadToEndAsync();

            //return new MomoResponse(mtid, jsonresponse);
            return resultString;
        }
        catch (WebException e)
        {
            return e.Message;
        }
    }

    public static async Task<string> SendConfirmPaymentRequest(string endpoint, string postJsonString)
    {
        try
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization",
                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("momo:momo")));
            var result = await client.PostAsync(endpoint, new StringContent(postJsonString, Encoding.UTF8, "application/json"));
            using var reader = new StreamReader(await result.Content.ReadAsStreamAsync());

            return await reader.ReadToEndAsync();
        }
        catch (WebException e)
        {
            return e.Message;
        }
    }
}