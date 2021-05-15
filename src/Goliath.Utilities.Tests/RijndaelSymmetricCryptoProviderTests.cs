using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class RijndaelSymmetricCryptoProviderTests
    {
        private const string cryptoKey = @"BGGymw22QmTEWS+o65PTgzEdUNI9tDWb6VQ+i3zWCEoa/ojxPvLHfYQGaFOkOCnx";
        [TestMethod]
        public void GenerateKey_generate_aes_key()
        {
            for (var i = 0; i < 15; i++)
            {
                var key = RijndaelSymmetricCryptoProvider.GenerateKey(256);
                Console.WriteLine(key);
            }
        }

        [TestMethod]
        public void encrypt_and_decrypt_verify_with_original_should_match()
        {
            var test = "Une idée qu’il a eu à émettre lors du récent forum d’Addis-Abeba autour de la problématique.  Au sujet du contrôle de l’exécutif, des entreprises et services public.";
            var rijndaelSymmetricCryptoProvider = new RijndaelSymmetricCryptoProvider(cryptoKey, 256);
            var encrypted = rijndaelSymmetricCryptoProvider.Encrypt(test.ConvertToByteArray());

            var rij = new RijndaelSymmetricCryptoProvider(cryptoKey, 256);
            var decrypted = rij.Decrypt(encrypted);

            Assert.AreEqual(test, decrypted.ConvertToString());
        }
    }
}
