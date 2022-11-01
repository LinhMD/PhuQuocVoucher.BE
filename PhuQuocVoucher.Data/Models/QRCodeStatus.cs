namespace PhuQuocVoucher.Data.Models;

public enum QRCodeStatus
{
    Active, //sitting there doing nothing
    Commit, //set when order been confirmed by provider
    Used,   //set when qr code get scan
    Disable //set when get delete
    
} 