using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goliath.Security;
using NUnit.Framework;

namespace Goliath.Utilities.Tests
{
    [TestFixture]
    public class RijndaelSymmetricCryptoProviderTests
    {
        private const string cryptoKey = @"apNkK4PPjDR1qSo5Yaxut2TzDsb5l80+t+IYK/RxBzYAdgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==";
        [Test]
        public void GenerateKey_generate_aes_key()
        {
            var key = RijndaelSymmetricCryptoProvider.GenerateKey(256);
            Console.WriteLine(key);
        }

        [Test]
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
