using System;
namespace Goliath.Security
{
    public class BCryptHashProvider : IHashProvider
    {
        public const string ProviderName = "BCrypt";

        public string Name => ProviderName;

        public byte[] ComputeHash(byte[] data)
        {
            throw new NotImplementedException();
        }

        public string ComputeHash(string secret)
        {
            return BCrypt.Net.BCrypt.HashPassword(secret);
        }

        public bool VerifyHash(string secret, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(secret, hash);
        }

        public byte[] ComputeHash(byte[] data, byte[] saltArray)
        {
            throw new NotImplementedException();
        }

        public bool VerifyHash(byte[] data, byte[] hash)
        {
            throw new NotImplementedException();
        }

        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            throw new NotImplementedException();
        }
    }
}