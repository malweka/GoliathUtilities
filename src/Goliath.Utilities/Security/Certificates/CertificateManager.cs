using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Goliath.Security
{
    public class CertificateManager : ICertificateManager
    {
        private X509Certificate2 signingCertificate;
        private static Dictionary<string, Func<X509Certificate2, byte[], byte[]>> supportedAlgorithms = new Dictionary<string, Func<X509Certificate2, byte[], byte[]>>();

        public string SigningAlgorithmName => signingCertificate?.SignatureAlgorithm.FriendlyName;

        static CertificateManager()
        {
            supportedAlgorithms.Add("SHA256RSA", CertSigner.SignSha256Rsa);
            supportedAlgorithms.Add("SHA256ECDSA", CertSigner.SignSha256Ecdsa);
        }

        public CertificateManager(X509Certificate2 signingCertificate)
        {
            LoadCertificate(signingCertificate);
        }

        public CertificateManager(string certificatePath)
        {
            var cert = CertificateExtensions.LoadCertificateFromPath(certificatePath);
            LoadCertificate(cert);
        }

        public CertificateManager(byte[] certificateBytes)
        {
            var cert = CertificateExtensions.LoadCertificate(certificateBytes);
            LoadCertificate(cert);
        }

        void LoadCertificate(X509Certificate2 certificate)
        {
            this.signingCertificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
            var signingAlgoName = certificate.SignatureAlgorithm.FriendlyName;

            if  (!this.signingCertificate.HasPrivateKey)
            {
                throw new InvalidOperationException("Certificate must contain private key to be able to sign.");
            }
        }

        public byte[] SignData(byte[] data)
        {
            var algorithm = signingCertificate.SignatureAlgorithm.FriendlyName;
            if (!supportedAlgorithms.TryGetValue(algorithm.ToUpper(), out var signFunction))
                throw new NotSupportedException(
                    $"Algorithm [{algorithm}] is currently not supported. Supported Algorithms: {string.Join(",", supportedAlgorithms.Keys)}");

            var signature = signFunction(signingCertificate, data);
            return signature;
        }
    }

    public interface ICertificateManager
    {
        string SigningAlgorithmName { get; }
        byte[] SignData(byte[] data);
    }
}