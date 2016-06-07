using System;
using System.Security;
using Microsoft.Owin.Security.DataProtection;

namespace Goliath.Security
{
    public class GoliathDataProtector : IDataProtector
    {
        private readonly Nancy.Cryptography.IHmacProvider hmacProvider;
        private readonly ISymmetricCryptoProvider cryptoProvider;

        public GoliathDataProtector(Nancy.Cryptography.IHmacProvider hmacProvider, ISymmetricCryptoProvider cryptoProvider)
        {
            this.hmacProvider = hmacProvider;
            this.cryptoProvider = cryptoProvider;
        }

        public byte[] Protect(byte[] userData)
        {
            var armoredData = cryptoProvider.Encrypt(userData);
            var hmacBytes = hmacProvider.GenerateHmac(armoredData);
            var signedArmoredData = SecurityHelperMethods.MergeByteArrays(hmacBytes, armoredData);

            return signedArmoredData;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            var armoredData = ExtractArmoredData(protectedData);
            var verifyBytes = hmacProvider.GenerateHmac(armoredData.Data);

            if (!string.Equals(verifyBytes.ConvertToBase64String(), armoredData.HashBytes.ConvertToBase64String()))
                throw new SecurityException("Could not verify the signature of the encrypted data.");

            var unprotectedData = cryptoProvider.Decrypt(armoredData.Data);
            return unprotectedData;
        }

        ArmoredDataInfo ExtractArmoredData(byte[] userData)
        {
            var sizeInBytes = hmacProvider.HmacLength / 8;
            if (userData.Length < sizeInBytes)
            {
                throw new ArgumentException("userData was not hashed with the HashAlgorithm submitted.");
            }

            var hmacBytes = new byte[sizeInBytes];
            var armoredData = new byte[userData.Length - sizeInBytes];

            for (int i = 0; i < hmacBytes.Length; i++)
            {
                hmacBytes[i] = userData[i];
            }

            for (int i = 0; i < armoredData.Length; i++)
            {
                armoredData[i] = userData[sizeInBytes + i];
            }

            var dataInfo = new ArmoredDataInfo { Data = armoredData, HashBytes = hmacBytes };
            return dataInfo;
        }

        struct ArmoredDataInfo
        {
            public byte[] Data;
            public byte[] HashBytes;
        }
    }

    public class GoliathDataProtectorProvider : IDataProtectionProvider
    {
        private readonly IDataProtector protector;

        public GoliathDataProtectorProvider(IDataProtector protector)
        {
            this.protector = protector;
        }

        public IDataProtector Create(params string[] purposes)
        {
            return protector;
        }
    }
}