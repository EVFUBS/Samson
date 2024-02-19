using System.Security.Cryptography;

namespace SamsonServer.Utility
{
    public static class TokenHelper
    {
        public static string CreateSecureRandomString(int count = 64) =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(count));

        public static string GenerateAuthorisationToken()
        {
            using (var cryptoServiceProvider = RandomNumberGenerator.Create())
            {
                var passwordBytes = GetRandomLengthByteArray(cryptoServiceProvider);
                cryptoServiceProvider.GetBytes(passwordBytes);
                return Convert.ToBase64String(passwordBytes);
            }
        }

        private static byte[] GetRandomLengthByteArray(RandomNumberGenerator cryptoServiceProvider)
        {
            var seedBytes = new byte[4];
            cryptoServiceProvider.GetBytes(seedBytes);
            var seed = BitConverter.ToInt32(seedBytes, 0);
            return new byte[new Random(seed).Next(50, 251)];
        }
    }
}
