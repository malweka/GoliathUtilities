using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X509;

namespace Goliath.Security
{
    public interface ICertificateGenerator
    {
        X509Certificate2 IssueCertificate(string subjectName, X509Certificate2 issuerCertificate, int expiresIn, string[] subjectAlternativeNames = null, 
            KeyPurposeID[] usages = null, string signatureAlgorithm = CertificateGenerator.SigningAlgorithms.Sha256WithRsa);
        X509Certificate2 CreateCertificateAuthorityCertificate(string subjectName, int expiresIn, string[] subjectAlternativeNames = null, 
            KeyPurposeID[] usages = null, string signatureAlgorithm = CertificateGenerator.SigningAlgorithms.Sha256WithRsa);
        X509Certificate2 CreateSelfSignedCertificate(string subjectName, int expiresIn, string[] subjectAlternativeNames = null, 
            KeyPurposeID[] usages = null, string signatureAlgorithm = CertificateGenerator.SigningAlgorithms.Sha256WithRsa);
    }
}