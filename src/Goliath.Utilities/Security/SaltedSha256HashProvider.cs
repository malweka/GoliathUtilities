using System;
using System.Security.Cryptography;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class SaltedSha256HashProvider : IHashProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public const string ProviderName = "SSHA256";

        #region IHashProvider Members

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return ProviderName; }
        }

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">data</exception>
        public byte[] ComputeHash(byte[] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException(nameof(data));
            var saltArray = SecurityHelperMethods.GenerateRandomSalt();
            return ComputeHash(data, saltArray);
        }

        public string ComputeHash(string secret)
        {
           if(string.IsNullOrEmpty(secret))
               throw new ArgumentNullException(nameof(secret));

           var hash = ComputeHash(secret.ConvertToByteArray());
           return hash.ConvertToBase64String();
        }

        public bool VerifyHash(string secret, string hash)
        {
            return VerifyHash(secret.ConvertToByteArray(), hash.ConvertFromBase64StringToByteArray());
        }

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="saltArray">The salt array.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">data</exception>
        public byte[] ComputeHash(byte[] data, byte[] saltArray)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException(nameof(data));
            if (saltArray == null || saltArray.Length == 0) throw new ArgumentNullException(nameof(saltArray));

            using (var managedHashProvider = new SHA256Managed())
            {
                var saltedData = SecurityHelperMethods.MergeByteArrays(data, saltArray);

                var hash = managedHashProvider.ComputeHash(saltedData);
                var saltedHash = SecurityHelperMethods.MergeByteArrays(hash, saltArray);

                return saltedHash;
            }
        }

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// data
        /// or
        /// hash
        /// </exception>
        public bool VerifyHash(byte[] data, byte[] hash)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException(nameof(data));
            if (hash == null || hash.Length == 0) throw new ArgumentNullException(nameof(hash));

            using (var managedHashProvider = new SHA256Managed())
            {
                var salt = managedHashProvider.ExtractRandomSalt(hash);
                var computedHash = ComputeHash(data, salt);
                return string.Equals(Convert.ToBase64String(hash), Convert.ToBase64String(computedHash));
            }
        }

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException(nameof(data));
            if (hash == null || hash.Length == 0) throw new ArgumentNullException(nameof(hash));
            if (salt == null || salt.Length == 0) throw new ArgumentNullException(nameof(salt));

            var computedHash = ComputeHash(data, salt);
            return string.Equals(Convert.ToBase64String(hash), Convert.ToBase64String(computedHash));
        }

        #endregion
    }
}