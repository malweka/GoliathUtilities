using System;
using System.Security.Cryptography;

namespace Goliath.Security
{
    public class HmacSha256Provider : IHmacProvider
    {

        public byte[] ComputeHash(byte[] secret, byte[] data)
        {
            using (var hmac = new HMACSHA256(secret))
            {
                return hmac.ComputeHash(data);
            }
        }

        public bool VerifyHash(byte[] secret, byte[] data, byte[] hash)
        {
            using (var hmac = new HMACSHA256(secret))
            {
                var computedHash = hmac.ComputeHash(data);
                return string.Equals(Convert.ToBase64String(hash), Convert.ToBase64String(computedHash));
            }
        }
    }
}