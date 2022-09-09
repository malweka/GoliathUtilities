using System;
using System.IO;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class CertificateManagerTests
    {
        [TestMethod]
        [DataRow("cert-ecdsa.pfx")]
        [DataRow("cert-rsa.pfx")]
        public void Ctor_load_from_path(string certFilename)
        {
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string priKey = Path.Combine(wkFolder, certFilename);

            var certManager = new CertificateManager(priKey);

            var cert = CertificateExtensions.LoadCertificateFromPath(priKey);
            Assert.AreEqual(cert.SignatureAlgorithm.FriendlyName, certManager.SigningAlgorithmName);
        }

        [TestMethod]
        [DataRow("cert-ecdsa.pfx")]
        [DataRow("cert-rsa.pfx")]
        public void Ctor_x509_from_parameter(string certFilename)
        {
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string priKey = Path.Combine(wkFolder, certFilename);
            var cert = CertificateExtensions.LoadCertificateFromPath(priKey);

            var certManager = new CertificateManager(cert);
            Assert.AreEqual(cert.SignatureAlgorithm.FriendlyName, certManager.SigningAlgorithmName);
        }

        [TestMethod]
        [DataRow("cert-ecdsa.pfx")]
        [DataRow("cert-rsa.pfx")]
        [DataRow("ca_cert_rsa.pfx")]
        public void Sign_happy_path(string certFileName)
        {
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string priKey = Path.Combine(wkFolder, certFileName);
            var certManager = new CertificateManager(priKey);

            var token = "daearerr23235d300doowmfdkjhoieoieoosdkofiej302935802fdsfaj#!@#2@3d@33".ConvertToByteArray();
            var signature = certManager.SignData(token).EncodeToBase64();

            Assert.IsFalse(string.IsNullOrWhiteSpace(signature));
        }
    }
}