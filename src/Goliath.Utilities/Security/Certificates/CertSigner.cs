using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Goliath.Security
{
    static class CertSigner
    {
        public static byte[] SignSha256Rsa(X509Certificate2 key, byte[] data)
        {
            var rsa = key.GetRSAPrivateKey();
            var signature = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return signature;
        }

        public static byte[] SignSha256Ecdsa(X509Certificate2 key, byte[] data)
        {
            var ecDsa = key.GetECDsaPrivateKey();
            var signature = ecDsa.SignData(data, HashAlgorithmName.SHA256);
            return signature;
        }
    }
}