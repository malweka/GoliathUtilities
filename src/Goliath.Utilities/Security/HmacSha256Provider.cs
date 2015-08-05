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

        public bool VerifyHash(byte[] secret, byte[] data, string hash)
        {
            var computed = ComputeHash(secret, data);
            var verify = Convert.ToBase64String(computed);
            return string.Equals(hash, verify);
        }
    }
}