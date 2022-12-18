using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class DsaAsymmetricCryptoProvider : CryptoProviderBase, IAsymmetricCryptoProvider
    {
        #region IAsymmetricCryptoProvider Members

        public string Name
        {
            get { return "DSA"; }
        }

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="keyFileName">Name of the key file.</param>
        /// <param name="keyStoreLocation">The key store location.</param>
        /// <exception cref="System.InvalidOperationException">keysize cannot be learger than 1024</exception>
        public void GenerateKeyPair(int keySize, string keyFileName, string keyStoreLocation)
        {
            using (var dsaProvider = new DSACryptoServiceProvider(keySize) { PersistKeyInCsp = false })
            {
                if (keySize > 1024)
                    throw new InvalidOperationException("keysize cannot be larger than 1024");

                CreateKey(dsaProvider, Path.Combine(keyStoreLocation, keyFileName + "_private.key"), true);
                CreateKey(dsaProvider, Path.Combine(keyStoreLocation, keyFileName + "_public.key"), false);
            }
        }

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <returns></returns>
        public CryptoKey GenerateKeyPair(int keySize)
        {
            using var provider = new DSACryptoServiceProvider(keySize) { PersistKeyInCsp = false };
            var key = new CryptoKey { PrivateKey = CreateKey(provider, true), PublicKey = CreateKey(provider, false) };
            return key;
        }

        /// <summary>
        /// Signs the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override byte[] Sign(string privateKey, Stream data)
        {
            using var provider = new DSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            var hash = provider.SignData(data);

            return hash;
        }

        /// <summary>
        /// Verifies the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override bool Verify(string publicKey, byte[] signature, byte[] data)
        {
            using var provider = new DSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            var result = provider.VerifyData(data, signature);

            return result;
        }

        /// <summary>
        /// Encrypts the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="data">The armoredData.</param>
        /// <returns></returns>
        public byte[] Encrypt(string publicKey, byte[] data)
        {
            throw new NotSupportedException("Encryption is not supported with DSA. Use RSA instead");
        }

        /// <summary>
        /// Decrypts the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="armoredData">The armoredData.</param>
        /// <returns></returns>
        public byte[] Decrypt(string privateKey, byte[] armoredData)
        {
            throw new NotSupportedException("Encryption is not supported with DSA. Use RSA instead");
        }

        #endregion
    }
}
