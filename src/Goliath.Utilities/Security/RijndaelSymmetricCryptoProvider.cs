using System;
using System.IO;
using System.Security.Cryptography;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class RijndaelSymmetricCryptoProvider : ISymmetricCryptoProvider
    {
        readonly byte[] keyArray;
        public const string ProviderName = "Rijndael";

        /// <summary>
        /// The block size
        /// </summary>
        public const int BlockSize = 128;

        static int BlockSizeInByte
        {
            get { return BlockSize / 8; }
        }

        /// <summary>
        /// Gets the size of the key.
        /// </summary>
        /// <value>
        /// The size of the key.
        /// </value>
        public int KeySize { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RijndaelSymmetricCryptoProvider"/> class.
        /// </summary>
        public RijndaelSymmetricCryptoProvider(ISettingsProvider settings) : this(settings?.GetEncryptionKey(ProviderName))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RijndaelSymmetricCryptoProvider"/> class.
        /// </summary>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <exception cref="System.ArgumentNullException">encryptionKey</exception>
        /// <exception cref="System.ArgumentException">Invalid encryption key</exception>
        public RijndaelSymmetricCryptoProvider(string encryptionKey, int keySize = 256)
        {
            if (string.IsNullOrEmpty(encryptionKey))
                throw new ArgumentNullException("encryptionKey");

            KeySize = keySize;
            keyArray = Convert.FromBase64String(encryptionKey);

            if (keyArray.Length < BlockSizeInByte)
                throw new ArgumentException("Invalid encryption key");
        }

        private byte[] Key
        {
            get
            {
                var keyActualSize = keyArray.Length - BlockSizeInByte;
                var key = new byte[keyActualSize];

                for (var i = 0; i < keyActualSize; i++)
                {
                    key[i] = keyArray[i];
                }

                return key;
            }
        }

        private byte[] IV
        {
            get
            {
                var ivStart = keyArray.Length - BlockSizeInByte;
                var iv = new byte[BlockSizeInByte];
                for (var i = 0; i < BlockSizeInByte; i++)
                {
                    iv[i] = keyArray[i + ivStart];
                }
                return iv;
            }
        }

        #region ISymmetricCryptoProvider Members

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => ProviderName;

        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <returns></returns>
        public static string GenerateKey(int keySize)
        {
            using (var rijProvider = new RijndaelManaged { KeySize = keySize, BlockSize = 128 })
            {
                rijProvider.GenerateKey();
                rijProvider.GenerateIV();

                var key = SecurityHelperMethods.MergeByteArrays(rijProvider.Key, rijProvider.IV);
                return Convert.ToBase64String(key);
            }
        }

        /// <summary>
        /// Encrypts the specified public key.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">data</exception>
        /// <exception cref="System.ArgumentException">data cannot be empty</exception>
        public byte[] Encrypt(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length <= 0) throw new ArgumentException("data cannot be empty");

            using (var memoryStream = new MemoryStream())
            {
                var key = Key;
                var iv = IV;
                using (var crypto = new RijndaelManaged { BlockSize = BlockSize, KeySize = KeySize })
                {
                    crypto.Key = key;
                    crypto.IV = iv;

                    using (var encryptor = crypto.CreateEncryptor(crypto.Key, crypto.IV))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }


        /// <summary>
        /// Decrypts the specified private key.
        /// </summary>
        /// <param name="armoredData">The armored data.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">armoredData</exception>
        /// <exception cref="System.ArgumentException">armoredData cannot be empty</exception>
        public byte[] Decrypt(byte[] armoredData)
        {
            if (armoredData == null) throw new ArgumentNullException("armoredData");
            if (armoredData.Length <= 0) throw new ArgumentException("armoredData cannot be empty");

            var key = Key;
            var iv = IV;

            using (var crypto = new RijndaelManaged() { BlockSize = BlockSize, KeySize = KeySize })
            {
                crypto.Key = key;
                crypto.IV = iv;

                using (var decryptor = crypto.CreateDecryptor(crypto.Key, crypto.IV))
                {
                    using (var memoryStream = new MemoryStream(armoredData))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            var data = cryptoStream.ReadToEnd();
                            return data;
                        }
                    }
                }
            }
        }

        #endregion
    }
}