using System.IO;
using System.Security.Cryptography;

namespace Goliath.Security
{
    public class RsaX509CryptoProvider : CryptoProviderBase, IAsymmetricCryptoProvider
    {
        #region IAsymmetricCryptoProvider Members

        public string Name => "RSA-X509";

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="keyFileName">Name of the key file.</param>
        /// <param name="keyStoreLocation">The key store location.</param>
        public void GenerateKeyPair(int keySize, string keyFileName, string keyStoreLocation)
        {
            using var provider = new RSACryptoServiceProvider(keySize) { PersistKeyInCsp = false };
            CreateKey(provider, Path.Combine(keyStoreLocation, keyFileName + "_private.key"), true);
            CreateKey(provider, Path.Combine(keyStoreLocation, keyFileName + "_public.key"), false);
        }

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <returns></returns>
        public CryptoKey GenerateKeyPair(int keySize)
        {
            using var provider = new RSACryptoServiceProvider(keySize) { PersistKeyInCsp = false };
            var key = new CryptoKey { PrivateKey = CreateKey(provider, true), PublicKey = CreateKey(provider, false) };
            return key;
        }

        /// <summary>
        /// Signs the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The armoredData.</param>
        /// <returns></returns>
        public override byte[] Sign(string privateKey, Stream data)
        {
            using var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            var hash = provider.SignData(data, CryptoConfig.MapNameToOID("SHA512"));

            return hash;
        }

        /// <summary>
        /// Verifies the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="data">The armoredData.</param>
        /// <returns></returns>
        public override bool Verify(string publicKey, byte[] signature, byte[] data)
        {
            using var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            var result = provider.VerifyData(data, CryptoConfig.MapNameToOID("SHA512"), signature);

            return result;
        }

        #endregion

        /// <summary>
        /// Encrypts the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="data">The armoredData.</param>
        /// <returns></returns>
        public byte[] Encrypt(string publicKey, byte[] data)
        {
            using var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            return provider.Encrypt(data, true);
        }

        /// <summary>
        /// Decrypts the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="armoredData">The armoredData.</param>
        /// <returns></returns>
        public byte[] Decrypt(string privateKey, byte[] armoredData)
        {
            using var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            return provider.Decrypt(armoredData, true);
        }
    }
}