using System;
using System.Security.Cryptography;

namespace Goliath.Security
{
    public class Pbkdf2HashProvider : IHashProvider
    {
        public const string ProviderName = "PBKDF2";
        protected int iterations = 301;
        protected int hashSize = 64;

        public string Name => ProviderName;

        public byte[] ComputeHash(byte[] data)
        {
            var saltArray = SecurityHelperMethods.GenerateRandomSalt();
            return ComputeHash(data, saltArray);
        }

        public string ComputeHash(string secret)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException(nameof(secret));

            var hash = ComputeHash(secret.ConvertToByteArray());
            return hash.ConvertToBase64String();
        }

        public bool VerifyHash(string secret, string hash)
        {
            return VerifyHash(secret.ConvertToByteArray(), hash.ConvertFromBase64StringToByteArray());
        }

        public byte[] ComputeHash(byte[] data, byte[] saltArray)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (saltArray == null) throw new ArgumentNullException(nameof(saltArray));

            using var derivedBytes = new Rfc2898DeriveBytes(data, saltArray, iterations);
            var hash = derivedBytes.GetBytes(hashSize);
            var saltedHash = SecurityHelperMethods.MergeByteArrays(hash, saltArray);
            return saltedHash;
        }

        public bool VerifyHash(byte[] data, byte[] hash)
        {
            var salt = SecurityHelperMethods.ExtractRandomSalt(hashSize, hash);
            return VerifyHash(data, hash, salt);
        }

        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (hash == null) throw new ArgumentNullException(nameof(hash));
            if (salt == null) throw new ArgumentNullException(nameof(salt));

            var computedHash = ComputeHash(data, salt);
            return string.Equals(Convert.ToBase64String(hash), Convert.ToBase64String(computedHash));
        }
    }
}