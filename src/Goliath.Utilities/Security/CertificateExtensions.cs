using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace Goliath.Security
{
    public static class CertificateExtensions
    {
        /// <summary>
        /// Add the Authority Key Identifier. According to http://www.alvestrand.no/objectid/2.5.29.35.html, this
        /// identifies the public key to be used to verify the signature on this certificate.
        /// In a certificate chain, this corresponds to the "Subject Key Identifier" on the *issuer* certificate.
        /// The Bouncy Castle documentation, at http://www.bouncycastle.org/wiki/display/JA1/X.509+Public+Key+Certificate+and+Certification+Request+Generation,
        /// shows how to create this from the issuing certificate. Since we're creating a self-signed certificate, we have to do this slightly differently.
        /// </summary>
        /// <param name="certificateGenerator"></param>
        /// <param name="issuerDN"></param>
        /// <param name="issuerKeyPair"></param>
        /// <param name="issuerSerialNumber"></param>
        public static void AddAuthorityKeyIdentifier(this X509V3CertificateGenerator certificateGenerator,
            X509Name issuerDN,
            AsymmetricCipherKeyPair issuerKeyPair,
            BigInteger issuerSerialNumber)
        {
            var authorityKeyIdentifierExtension =
                new AuthorityKeyIdentifier(
                    SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(issuerKeyPair.Public),
                    new GeneralNames(new GeneralName(issuerDN)),
                    issuerSerialNumber);
            certificateGenerator.AddExtension(
                X509Extensions.AuthorityKeyIdentifier.Id, false, authorityKeyIdentifierExtension);
        }

        /// <summary>
        /// Add the "Subject Alternative Names" extension. Note that you have to repeat
        /// the value from the "Subject Name" property.
        /// </summary>
        /// <param name="certificateGenerator"></param>
        /// <param name="subjectAlternativeNames"></param>
        public static void AddSubjectAlternativeNames(this X509V3CertificateGenerator certificateGenerator,
            IEnumerable<string> subjectAlternativeNames)
        {
            var subjectAlternativeNamesExtension =
                new DerSequence(
                    subjectAlternativeNames.Select(name => new GeneralName(GeneralName.DnsName, name))
                        .ToArray<Asn1Encodable>());

            certificateGenerator.AddExtension(
                X509Extensions.SubjectAlternativeName.Id, false, subjectAlternativeNamesExtension);
        }

        /// <summary>
        /// Add the "Extended Key Usage" extension, specifying (for example) "server authentication".
        /// </summary>
        /// <param name="certificateGenerator"></param>
        /// <param name="usages"></param>
        public static void AddExtendedKeyUsage(this X509V3CertificateGenerator certificateGenerator, KeyPurposeID[] usages)
        {
            certificateGenerator.AddExtension(
                X509Extensions.ExtendedKeyUsage.Id, false, new ExtendedKeyUsage(usages));
        }

        /// <summary>
        /// Add the "Basic Constraints" extension.
        /// </summary>
        /// <param name="certificateGenerator"></param>
        /// <param name="isCertificateAuthority"></param>
        public static void AddBasicConstraints(this X509V3CertificateGenerator certificateGenerator,
            bool isCertificateAuthority)
        {
            certificateGenerator.AddExtension(
                X509Extensions.BasicConstraints.Id, true, new BasicConstraints(isCertificateAuthority));
        }

        /// <summary>
        /// Add the Subject Key Identifier.
        /// </summary>
        /// <param name="certificateGenerator"></param>
        /// <param name="subjectKeyPair"></param>
        public static void AddSubjectKeyIdentifier(this X509V3CertificateGenerator certificateGenerator,
            AsymmetricCipherKeyPair subjectKeyPair)
        {
            var subjectKeyIdentifierExtension =
                new SubjectKeyIdentifier(
                    SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(subjectKeyPair.Public));
            certificateGenerator.AddExtension(
                X509Extensions.SubjectKeyIdentifier.Id, false, subjectKeyIdentifierExtension);
        }

        public static X509Certificate2 ConvertCertificate(this X509Certificate certificate,
            AsymmetricCipherKeyPair subjectKeyPair,
            Org.BouncyCastle.Security.SecureRandom random)
        {
            // Now to convert the Bouncy Castle certificate to a .NET certificate.
            // See http://web.archive.org/web/20100504192226/http://www.fkollmann.de/v2/post/Creating-certificates-using-BouncyCastle.aspx
            // ...but, basically, we create a PKCS12 store (a .PFX file) in memory, and add the public and private key to that.
            var store = new Pkcs12Store();

            // What Bouncy Castle calls "alias" is the same as what Windows terms the "friendly name".
            string friendlyName = certificate.SubjectDN.ToString();

            // Add the certificate.
            var certificateEntry = new X509CertificateEntry(certificate);
            store.SetCertificateEntry(friendlyName, certificateEntry);

            // Add the private key.
            store.SetKeyEntry(friendlyName, new AsymmetricKeyEntry(subjectKeyPair.Private), new[] { certificateEntry });

            // ConvertToEnvironmentType it to an X509Certificate2 object by saving/loading it from a MemoryStream.
            // It needs a password. Since we'll remove this later, it doesn't particularly matter what we use.
            const string password = "password";
            var stream = new MemoryStream();
            store.Save(stream, password.ToCharArray(), random);

            var convertedCertificate =
                new X509Certificate2(stream.ToArray(),
                    password,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            return convertedCertificate;
        }

        /// <summary>
        /// Loads a certificate from file path.
        /// </summary>
        /// <param name="certificatePath">certificate file path</param>
        /// <returns></returns>
        public static X509Certificate2 LoadCertificateFromPath(string certificatePath)
        {
            if (!File.Exists(certificatePath))
                throw new InvalidOperationException($"Certificate file {certificatePath} was not found.");

            return new X509Certificate2(certificatePath, "", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
        }

        /// <summary>
        /// Loads a certificate from a byte array.
        /// </summary>
        /// <param name="certBytes">The cert bytes.</param>
        /// <returns></returns>
        public static X509Certificate2 LoadCertificate(byte[] certBytes)
        {
            var numGen = new UniqueLongGenerator();
            var file = Path.Combine(Path.GetTempPath(), $"x509cert-{numGen.GetNextId()}");
            try
            {
                File.WriteAllBytes(file, certBytes);
                return new X509Certificate2(file, "", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            }
            finally
            {
                File.Delete(file);
            }
        }

        public static X509Certificate2 LoadPemCertificate(string pemCertificate)
        {
            if (string.IsNullOrWhiteSpace(pemCertificate))
                return null;

            string[] split = pemCertificate.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string base64CertText;

            if (split.Length > 1 && split[0].Contains("BEGIN"))
            {
                base64CertText = string.Join(string.Empty, split, 1, split.Length - 2);
            }
            else
            {
                base64CertText = pemCertificate;
            }

            var certBytes = Convert.FromBase64String(base64CertText);
            return LoadCertificate(certBytes);
        }

        public static string ConvertToPem(this X509Certificate2 certificate)
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(certificate.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END CERTIFICATE-----");
            return builder.ToString();
        }

        public static void WriteCertificateToFile(this X509Certificate2 certificate, string outputFileName, string password = null)
        {
            if (certificate == null) throw new ArgumentNullException(nameof(certificate));
            var bytes = certificate.Export(X509ContentType.Pfx, password);
            File.WriteAllBytes(outputFileName, bytes);
        }

        public static string WriteCertificateToString(this X509Certificate2 certificate, string password = null)
        {
            if (certificate == null) throw new ArgumentNullException(nameof(certificate));
            var bytes = certificate.Export(X509ContentType.Pkcs12, password);
            var stringCert = Convert.ToBase64String(bytes);
            return stringCert;
        }
    }
}