using FirebaseAdmin.Auth;

namespace PhuQuocVoucher.Api.Ultility
{
    public static class FirebaseUtility
    {
        private static async Task<UserRecord> GetFireBaseUser(string Uid)
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(Uid);
            return userRecord;
        }

        public static async Task<UserRecord> GetFireBaseUserByToken(string token)
        {
            
            var decode = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

            var uid = decode.Uid;

            return await GetFireBaseUser(uid);
        }

    }
}
