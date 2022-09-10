using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Goliath.Security
{
    abstract class CertificateSigner : ICertificateSigner
    {
        protected X509Certificate2 certificate;
        protected HashAlgorithmName hashAlgorithm;

        protected CertificateSigner(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm)
        {
            this.certificate = certificate;
            this.hashAlgorithm = hashAlgorithm;
        }

        public abstract byte[] SignData(byte[] data);

        public abstract bool Verify(byte[] signature, byte[] data);
    }
}