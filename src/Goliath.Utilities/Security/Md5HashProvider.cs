using System;
using System.Security.Cryptography;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class Md5HashProvider : IHashProvider
    {
        public const string ProviderName = "MD5";

        public string Name { get { return ProviderName; } }

        public byte[] ComputeHash(byte[] data)
        {
            using (var managedHashProvider = new MD5CryptoServiceProvider())
            {
                var hash = managedHashProvider.ComputeHash(data);
                return hash;
            }
        }

        public byte[] ComputeHash(byte[] data, byte[] saltArray)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public bool VerifyHash(byte[] data, byte[] hash)
        {
            var computedHash = ComputeHash(data);
            return string.Equals(Convert.ToBase64String(hash), Convert.ToBase64String(computedHash));
        }

        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            throw new NotImplementedException();
        }
    }
}