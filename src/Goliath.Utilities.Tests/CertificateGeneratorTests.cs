using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class CertificateGeneratorTests
    {
        [TestMethod]

        public void Generate_CA_certificate_default_algorithm()
        {
            CertificateGenerator certGenerator = new CertificateGenerator();
            var cert = certGenerator.CreateCertificateAuthorityCertificate("CN=Test CA", 10);
            Assert.IsNotNull(cert);
        }

        [TestMethod]
        [DataRow(CertificateGenerator.SigningAlgorithms.Sha256WithRsa)]
        [DataRow(CertificateGenerator.SigningAlgorithms.Sha384WithEcdsa)]
        public void Generate_CA_certificate_with_algorithms(string algorithm)
        {
            CertificateGenerator certGenerator = new CertificateGenerator();
            var cert = certGenerator.CreateCertificateAuthorityCertificate("CN=Test CA", 10, null, null, algorithm);
            Assert.IsNotNull(cert);
        }

        [TestMethod]
        //[DataRow(CertificateGenerator.SigningAlgorithms.Sha256WithRsa)]
        [DataRow(CertificateGenerator.SigningAlgorithms.Sha384WithEcdsa)]
        public void Issue_certificate_using_CA_cert(string signingAlgorithm)
        {
            CertificateGenerator certGenerator = new CertificateGenerator();
            var caCert = certGenerator.CreateCertificateAuthorityCertificate("CN=Test CA", 10, null, null, signingAlgorithm);

            string fileName = "rsa";
            if (signingAlgorithm == CertificateGenerator.SigningAlgorithms.Sha384WithEcdsa)
                fileName = "ecdsa";

            caCert.WriteCertificateToFile($"C:\\junk\\ca_cert_{fileName}.pfx");

            var cert = certGenerator.IssueCertificate("CN=Test Cert", caCert, 5, null, null, signingAlgorithm);
            cert.WriteCertificateToFile($"C:\\junk\\cert_{fileName}.pfx");

            Assert.IsNotNull(cert);
        }

    }
}