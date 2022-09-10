using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Goliath.Security
{
    class RsaShaSigner : CertificateSigner
    {
        public RsaShaSigner(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm) : base(certificate, hashAlgorithm)
        {
        }

        public override byte[] SignData(byte[] data)
        {
            var rsa = certificate.GetRSAPrivateKey();
            var signature = rsa.SignData(data, hashAlgorithm, RSASignaturePadding.Pkcs1);
            return signature;
        }

        public override bool Verify(byte[] signature, byte[] data)
        {
            var rsa = certificate.GetRSAPrivateKey();
            return rsa.VerifyData(data, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
        }
    }
}