namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        byte[] ComputeHash(byte[] data);

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        string ComputeHash(string secret);

        /// <summary>
        /// Verify hash
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool VerifyHash(string secret, string hash);

        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="saltArray">The salt array.</param>
        /// <returns></returns>
        byte[] ComputeHash(byte[] data, byte[] saltArray);

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        bool VerifyHash(byte[] data, byte[] hash);

        /// <summary>
        /// Verifies the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        bool VerifyHash(byte[] data, byte[] hash, byte[] salt);
    }
}