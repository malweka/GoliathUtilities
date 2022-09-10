using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Goliath.Security
{
    class EcDsaShaSigner : CertificateSigner
    {
        public EcDsaShaSigner(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm) : base(certificate, hashAlgorithm)
        {
        }

        public override byte[] SignData(byte[] data)
        {
            var rsa = certificate.GetECDsaPrivateKey();
            var signature = rsa.SignData(data, hashAlgorithm);
            return signature;
        }

        public override bool Verify(byte[] signature, byte[] data)
        {
            var rsa = certificate.GetECDsaPrivateKey();
            return rsa.VerifyData(data, signature, hashAlgorithm);
        }
    }
}