using System;
using System.Linq;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using X509Certificate2 = System.Security.Cryptography.X509Certificates.X509Certificate2;
using X509KeyStorageFlags = System.Security.Cryptography.X509Certificates.X509KeyStorageFlags;

namespace Goliath.Security
{
    public class CertificateGenerator : ICertificateGenerator
    {
        static string[] alternativeNames = new string[] { "hamsman", "hamsman.com" };

        public static X509Certificate2 LoadCertificateFromFile(string issuerFileName, string password)
        {
            // We need to pass 'Exportable', otherwise we can't get the private key.
            var issuerCertificate = new X509Certificate2(issuerFileName, password, X509KeyStorageFlags.Exportable);
            return issuerCertificate;
        }

        public static X509Certificate2 LoadCertificateFromString(string certificateText, string password)
        {
            var certBytes = Convert.FromBase64String(certificateText);
            return new X509Certificate2(certBytes, password, X509KeyStorageFlags.Exportable);
        }

        public X509Certificate2 IssueCertificate(string subjectName, X509Certificate2 issuerCertificate, int expiresIn, string[] subjectAlternativeNames = null, KeyPurposeID[] usages = null)
        {
            // It's self-signed, so these are the same.
            var issuerName = issuerCertificate.Subject;

            if (subjectAlternativeNames == null || subjectAlternativeNames.Length == 0)
                subjectAlternativeNames = alternativeNames;

            if (usages == null || usages.Length == 0)
                usages = new[] { KeyPurposeID.IdKPServerAuth };

            var random = GetSecureRandom();
            var subjectKeyPair = GenerateKeyPair(random, 3072);

            var issuerKeyPair = DotNetUtilities.GetKeyPair(issuerCertificate.PrivateKey);

            var serialNumber = GenerateSerialNumber(random);
            var issuerSerialNumber = new BigInteger(issuerCertificate.GetSerialNumber());

            const bool isCertificateAuthority = false;
            var certificate = GenerateCertificate(random, subjectName, subjectKeyPair, serialNumber,
                                                  subjectAlternativeNames, issuerName, issuerKeyPair,
                                                  issuerSerialNumber, isCertificateAuthority,
                                                  usages, expiresIn);
            return certificate.ConvertCertificate(subjectKeyPair, random);
        }

        public X509Certificate2 CreateCertificateAuthorityCertificate(string subjectName, int expiresIn, string[] subjectAlternativeNames = null, KeyPurposeID[] usages = null)
        {
            // It's self-signed, so these are the same.
            var issuerName = subjectName;

            if (subjectAlternativeNames == null || subjectAlternativeNames.Length == 0)
                subjectAlternativeNames = alternativeNames;

            if (usages == null || usages.Length == 0)
                usages = new[] { KeyPurposeID.IdKPServerAuth };

            var random = GetSecureRandom();
            var subjectKeyPair = GenerateKeyPair(random, 3072);

            // It's self-signed, so these are the same.
            var issuerKeyPair = subjectKeyPair;

            var serialNumber = GenerateSerialNumber(random);
            var issuerSerialNumber = serialNumber; // Self-signed, so it's the same serial number.

            const bool isCertificateAuthority = true;
            var certificate = GenerateCertificate(random, subjectName, subjectKeyPair, serialNumber,
                                                  subjectAlternativeNames, issuerName, issuerKeyPair,
                                                  issuerSerialNumber, isCertificateAuthority,
                                                  usages, expiresIn);
            return certificate.ConvertCertificate(subjectKeyPair, random);
        }

        public X509Certificate2 CreateSelfSignedCertificate(string subjectName, int expiresIn, string[] subjectAlternativeNames = null, KeyPurposeID[] usages = null)
        {
            // It's self-signed, so these are the same.
            var issuerName = subjectName;

            if (subjectAlternativeNames == null || subjectAlternativeNames.Length == 0)
                subjectAlternativeNames = alternativeNames;

            if (usages == null || usages.Length == 0)
                usages = new[] { KeyPurposeID.IdKPServerAuth };

            var random = GetSecureRandom();
            var subjectKeyPair = GenerateKeyPair(random, 3072);

            // It's self-signed, so these are the same.
            var issuerKeyPair = subjectKeyPair;

            var serialNumber = GenerateSerialNumber(random);
            var issuerSerialNumber = serialNumber; // Self-signed, so it's the same serial number.

            const bool isCertificateAuthority = false;
            var certificate = GenerateCertificate(random, subjectName, subjectKeyPair, serialNumber,
                                                  subjectAlternativeNames, issuerName, issuerKeyPair,
                                                  issuerSerialNumber, isCertificateAuthority,
                                                  usages, expiresIn);
            return certificate.ConvertCertificate(subjectKeyPair, random);
        }

        private static Org.BouncyCastle.Security.SecureRandom GetSecureRandom()
        {
            var randomGenerator = new CryptoApiRandomGenerator();
            var random = new Org.BouncyCastle.Security.SecureRandom(randomGenerator);
            return random;
        }

        private static X509Certificate GenerateCertificate(Org.BouncyCastle.Security.SecureRandom random,
            string subjectName,
            AsymmetricCipherKeyPair subjectKeyPair,
            BigInteger subjectSerialNumber,
            string[] subjectAlternativeNames,
            string issuerName,
            AsymmetricCipherKeyPair issuerKeyPair,
            BigInteger issuerSerialNumber,
            bool isCertificateAuthority,
            KeyPurposeID[] usages,
            int expiresIn)
        {
            var certificateGenerator = new X509V3CertificateGenerator();

            certificateGenerator.SetSerialNumber(subjectSerialNumber);

            // Set the signature algorithm. This is used to generate the thumbprint which is then signed
            // with the issuer's private key. We'll use SHA-256, which is (currently) considered fairly strong.
            const string signatureAlgorithm = "SHA256WithRSA";
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);

            var issuerDN = new X509Name(issuerName);
            certificateGenerator.SetIssuerDN(issuerDN);

            // Note: The subject can be omitted if you specify a subject alternative name (SAN).
            var subjectDN = new X509Name(subjectName);
            certificateGenerator.SetSubjectDN(subjectDN);

            // Our certificate needs valid from/to values.
            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(expiresIn);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            // The subject's public key goes in the certificate.
            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            certificateGenerator.AddAuthorityKeyIdentifier(issuerDN, issuerKeyPair, issuerSerialNumber);
            certificateGenerator.AddSubjectKeyIdentifier(subjectKeyPair);
            certificateGenerator.AddBasicConstraints(isCertificateAuthority);

            if (usages != null && usages.Any())
                certificateGenerator.AddExtendedKeyUsage(usages);

            if (subjectAlternativeNames != null && subjectAlternativeNames.Any())
                certificateGenerator.AddSubjectAlternativeNames(subjectAlternativeNames);

            // The certificate is signed with the issuer's private key.
            var certificate = certificateGenerator.Generate(issuerKeyPair.Private, random);
            return certificate;
        }

        /// <summary>
        /// The certificate needs a serial number. This is used for revocation,
        /// and usually should be an incrementing index (which makes it easier to revoke a range of certificates).
        /// Since we don't have anywhere to store the incrementing index, we can just use a random number.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        private static BigInteger GenerateSerialNumber(Org.BouncyCastle.Security.SecureRandom random)
        {
            var serialNumber =
                BigIntegers.CreateRandomInRange(
                    BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            return serialNumber;
        }

        /// <summary>
        /// Generate a key pair.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        /// <param name="strength">The key length in bits. For RSA, 2048 bits should be considered the minimum acceptable these days.</param>
        /// <returns></returns>
        private static AsymmetricCipherKeyPair GenerateKeyPair(Org.BouncyCastle.Security.SecureRandom random, int strength)
        {
            var keyGenerationParameters = new KeyGenerationParameters(random, strength);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            var subjectKeyPair = keyPairGenerator.GenerateKeyPair();
            return subjectKeyPair;
        }
    }
}