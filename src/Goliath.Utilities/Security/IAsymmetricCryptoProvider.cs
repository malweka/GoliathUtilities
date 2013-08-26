using System.IO;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsymmetricCryptoProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="keyFileName">Name of the key file.</param>
        /// <param name="keyStorePath">The key store path.</param>
        void GenerateKeyPair(int keySize, string keyFileName, string keyStorePath);

        /// <summary>
        /// Generates the key pair.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <returns></returns>
        CryptoKey GenerateKeyPair(int keySize);

        #region Sign 

        /// <summary>
        /// Signs the specified data using the private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data to sign.</param>
        /// <returns></returns>
        byte[] Sign(Stream privateKey, string data);

        /// <summary>
        /// Signs the specified data using the private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data to sign.</param>
        /// <returns></returns>
        byte[] Sign(Stream privateKey, Stream data);

        /// <summary>
        /// Signs the specified data using the private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        byte[] Sign(string privateKey, Stream data);

        /// <summary>
        /// Signs the specified data using the private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        byte[] Sign(string privateKey, string data);

        #endregion

        #region Verify

        /// <summary>
        /// Verifies the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        bool Verify(string publicKey, string signature, Stream data);

        /// <summary>
        /// Verifies the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        bool Verify(string publicKey, byte[] signature, byte[] data);

        #endregion

        /// <summary>
        /// Encrypts the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        byte[] Encrypt(string publicKey, byte[] data);

        /// <summary>
        /// Decrypts the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="armoredData">The armored data.</param>
        /// <returns></returns>
        byte[] Decrypt(string privateKey, byte[] armoredData);
    }
}
