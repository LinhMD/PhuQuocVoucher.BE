﻿using System.Security.Cryptography;
using System.Text;

namespace PhuQuocVoucher.Business.Services.Implements;

public static class MoMoSecurity
{
    private static RNGCryptoServiceProvider _rngCsp = new ();
#pragma warning restore CS0618

    public static string  GetHash(string partnerCode, string merchantRefId, string amount, string paymentCode, string storeId,
        string storeName, string publicKeyXml)
    {
        var json = "{\"partnerCode\":\"" + partnerCode + "\",\"partnerRefId\":\"" + merchantRefId + "\",\"amount\":" +
                   amount + ",\"paymentCode\":\"" + paymentCode + "\",\"storeId\":\"" + storeId +
                   "\",\"storeName\":\"" + storeName + "\"}";
        var data = Encoding.UTF8.GetBytes(json);
        string? result = null;
        using var rsa = new RSACryptoServiceProvider(4096);
        try
        {
            // MoMo's public key has format PEM.
            // You must convert it to XML format. Recommend tool: https://superdry.apphb.com/tools/online-rsa-key-converter
            rsa.FromXmlString(publicKeyXml);
            var encryptedData = rsa.Encrypt(data, false);
            var base64Encrypted = Convert.ToBase64String(encryptedData);
            result = base64Encrypted;
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }

        return result;
    }

    public static string BuildQueryHash(string partnerCode, string merchantRefId, string requestid, string publicKey)
    {
        var json = "{\"partnerCode\":\"" + partnerCode + "\",\"partnerRefId\":\"" + merchantRefId +
                   "\",\"requestId\":\"" + requestid + "\"}";
        var data = Encoding.UTF8.GetBytes(json);
        string result;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {
                // client encrypting data with public key issued by server
                rsa.FromXmlString(publicKey);
                var encryptedData = rsa.Encrypt(data, false);
                var base64Encrypted = Convert.ToBase64String(encryptedData);
                result = base64Encrypted;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        return result;
    }

    public static string BuildRefundHash(string partnerCode, string merchantRefId, string momoTranId, long amount,
        string description, string publicKey)
    {
        var json = "{\"partnerCode\":\"" + partnerCode + "\",\"partnerRefId\":\"" + merchantRefId +
                   "\",\"momoTransId\":\"" + momoTranId + "\",\"amount\":" + amount + ",\"description\":\"" +
                   description + "\"}";
        var data = Encoding.UTF8.GetBytes(json);
        string result;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {
                // client encrypting data with public key issued by server
                rsa.FromXmlString(publicKey);
                var encryptedData = rsa.Encrypt(data, false);
                var base64Encrypted = Convert.ToBase64String(encryptedData);
                result = base64Encrypted;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        return result;
    }

    public static string SignSha256(string message, string key)
    {
        var keyByte = Encoding.UTF8.GetBytes(key);
        var messageBytes = Encoding.UTF8.GetBytes(message);
        using var hash256 = new HMACSHA256(keyByte);
        var hashMessage = hash256.ComputeHash(messageBytes);
        var hex = BitConverter.ToString(hashMessage);
        hex = hex.Replace("-", "").ToLower();
        return hex;
    }
}