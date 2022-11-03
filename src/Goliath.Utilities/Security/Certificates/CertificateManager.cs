using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Goliath.Security
{
    public class CertificateManager : ICertificateManager
    {
        private X509Certificate2 signingCertificate;
        private static readonly Dictionary<string, Func<X509Certificate2, ICertificateSigner>> supportedAlgorithms =
            new Dictionary<string, Func<X509Certificate2, ICertificateSigner>>();

        public string SigningAlgorithmName => signingCertificate?.SignatureAlgorithm.FriendlyName;

        static CertificateManager()
        {
            supportedAlgorithms.Add("SHA256RSA", cert => new RsaShaSigner(cert, HashAlgorithmName.SHA256));
            supportedAlgorithms.Add("SHA256ECDSA", cert => new EcDsaShaSigner(cert, HashAlgorithmName.SHA256));
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

            if (!this.signingCertificate.HasPrivateKey)
            {
                throw new InvalidOperationException("Certificate must contain private key to be able to sign.");
            }
        }

        public byte[] SignData(byte[] data)
        {
            if (!supportedAlgorithms.TryGetValue(SigningAlgorithmName.ToUpper(), out var createSignerFunc))
                throw new NotSupportedException(
                    $"Algorithm [{SigningAlgorithmName}] is currently not supported. Supported Algorithms: {string.Join(",", supportedAlgorithms.Keys)}");

            ICertificateSigner signer = createSignerFunc(signingCertificate);

            var signature = signer.SignData(data);
            return signature;
        }

        public bool VerifySignature(byte[] signature, byte[] data)
        {
            if (!supportedAlgorithms.TryGetValue(SigningAlgorithmName.ToUpper(), out var createSignerFunc))
                throw new NotSupportedException(
                    $"Algorithm [{SigningAlgorithmName}] is currently not supported. Supported Algorithms: {string.Join(",", supportedAlgorithms.Keys)}");

            ICertificateSigner signer = createSignerFunc(signingCertificate);

            return signer.Verify(signature, data);
        }
    }
}