using System;
using System.Security.Cryptography;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class HmacSha512Provider : IHmacProvider
    {

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public byte[] ComputeHash(byte[] secret, byte[] data)
        {
            using (var hmac = new HMACSHA512(secret))
            {
                return hmac.ComputeHash(data);
            }
        }

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public bool VerifyHash(byte[] secret, byte[] data, byte[] hash)
        {
            using (var hmac = new HMACSHA512(secret))
            {
                var computedHash = hmac.ComputeHash(data);
                return string.Equals(Convert.ToBase64String(hash), Convert.ToBase64String(computedHash));
            }
        }
    }
}