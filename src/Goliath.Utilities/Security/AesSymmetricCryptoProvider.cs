using System;
using System.IO;
using System.Security.Cryptography;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class AesSymmetricCryptoProvider : ISymmetricCryptoProvider
    {
        readonly byte[] keyArray;

        /// <summary>
        /// The block size
        /// </summary>
        public const int BlockSize = 256;

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
        /// Initializes a new instance of the <see cref="AesSymmetricCryptoProvider"/> class.
        /// </summary>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <exception cref="System.ArgumentNullException">encryptionKey</exception>
        /// <exception cref="System.ArgumentException">Invalid encryption key</exception>
        public AesSymmetricCryptoProvider(string encryptionKey, int keySize)
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
        public string Name { get { return "AES"; } }

        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <param name="keySize">Size of the key.</param>
        /// <returns></returns>
        public static string GenerateKey(int keySize)
        {
            using (var aesProvider = new AesManaged { KeySize = keySize, BlockSize = 256 })
            {
                aesProvider.GenerateKey();
                aesProvider.GenerateIV();

                var key = SecurityHelperMethods.MergeByteArrays(aesProvider.Key, aesProvider.IV);
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
                using (var aesCrypto = new AesManaged { BlockSize = BlockSize, KeySize = KeySize })
                {
                    aesCrypto.Key = key;
                    aesCrypto.IV = iv;

                    using (var encryptor = aesCrypto.CreateEncryptor(aesCrypto.Key, aesCrypto.IV))
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

            using (var aesCrypto = new AesManaged() { BlockSize = BlockSize, KeySize = KeySize })
            {
                aesCrypto.Key = key;
                aesCrypto.IV = iv;

                using (var decryptor = aesCrypto.CreateDecryptor(aesCrypto.Key, aesCrypto.IV))
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