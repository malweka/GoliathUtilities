namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISymmetricCryptoProvider
    {
        /// <summary>
        /// Gets the size of the key.
        /// </summary>
        /// <value>
        /// The size of the key.
        /// </value>
        int KeySize { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Encrypts the specified public key.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decrypts the specified private key.
        /// </summary>
        /// <param name="armoredData">The armored data.</param>
        /// <returns></returns>
        byte[] Decrypt(byte[] armoredData);
    }
}