using Nancy.Cryptography;

namespace Goliath.Security
{
    public class GoliathRijndaelProvider : IEncryptionProvider
    {
        private readonly ISymmetricCryptoProvider crypto;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoliathRijndaelProvider"/> class.
        /// </summary>
        /// <param name="crypto">The crypto.</param>
        public GoliathRijndaelProvider(ISymmetricCryptoProvider crypto)
        {
            this.crypto = crypto;
        }

        public string Encrypt(string data)
        {
            var dataByte = data.ConvertToByteArray();
            var encrypted = crypto.Encrypt(dataByte);
            return encrypted.ConvertToBase64String();
        }

        public string Decrypt(string data)
        {
            var dataByte = data.ConvertToByteArray();
            var decrypted = crypto.Decrypt(dataByte);
            return decrypted.ConvertToString();
        }
    }
}