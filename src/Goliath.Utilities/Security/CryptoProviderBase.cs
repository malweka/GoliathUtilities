﻿using System;
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
    public abstract class CryptoProviderBase
    {
        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="asymmetricAlgorithm">The asymmetric algorithm.</param>
        /// <param name="keyFile">The key file.</param>
        /// <param name="isPrivateKey">if set to <c>true</c> [is private key].</param>
        protected void CreateKey(AsymmetricAlgorithm asymmetricAlgorithm, string keyFile, bool isPrivateKey)
        {
            using var privateStream = new FileStream(keyFile, FileMode.Create, FileAccess.ReadWrite);
            CreateKey(asymmetricAlgorithm, privateStream, isPrivateKey);
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="asymmetricAlgorithm">The asymmetric algorithm.</param>
        /// <param name="privateStream">The private stream.</param>
        /// <param name="isPrivateKey">if set to <c>true</c> [is private key].</param>
        /// <exception cref="System.ArgumentNullException">privateStream</exception>
        protected void CreateKey(AsymmetricAlgorithm asymmetricAlgorithm, Stream privateStream, bool isPrivateKey)
        {
            if (privateStream == null) throw new ArgumentNullException(nameof(privateStream));

            using var streamWriter = new StreamWriter(privateStream);
            streamWriter.Write(CreateKey(asymmetricAlgorithm, isPrivateKey));
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="asymmetricAlgorithm">The asymmetric algorithm.</param>
        /// <param name="isPrivateKey">if set to <c>true</c> [is private key].</param>
        /// <returns></returns>
        protected string CreateKey(AsymmetricAlgorithm asymmetricAlgorithm, bool isPrivateKey)
        {
            return asymmetricAlgorithm.ToXmlString(isPrivateKey);
        }

        /// <summary>
        /// Signs the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public virtual byte[] Sign(Stream privateKey, string data)
        {
            using var dataStream = new MemoryStream(data.ConvertToByteArray());
            var hash = Sign(privateKey, dataStream);
            return hash;
        }

        /// <summary>
        /// Signs the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public virtual byte[] Sign(Stream privateKey, Stream data)
        {
            using var reader = new StreamReader(privateKey);
            var hash = Sign(reader.ReadToEnd(), data);

            return hash;
        }

        /// <summary>
        /// Signs the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public virtual byte[] Sign(string privateKey, string data)
        {
            using var dataStream = new MemoryStream(data.ConvertToByteArray());
            var hash = Sign(privateKey, dataStream);
            return hash;
        }

        /// <summary>
        /// Signs the specified private key.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public abstract byte[] Sign(string privateKey, Stream data);


        /// <summary>
        /// Verifies the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public virtual bool Verify(string publicKey, string signature, Stream data)
        {
            var result = Verify(publicKey, Convert.FromBase64String(signature), data.ReadToEnd());
            return result;
        }

        /// <summary>
        /// Verifies the specified public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public abstract bool Verify(string publicKey, byte[] signature, byte[] data);
    }
}
