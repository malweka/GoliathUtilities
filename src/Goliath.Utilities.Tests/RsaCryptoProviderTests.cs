using System;
using System.IO;
using NUnit.Framework;
using Goliath.Security;

namespace Goliath.Utilities.Tests
{
    [TestFixture]
    public class RsaCryptoProviderTests
    {
        [Test]
        public void GenerateKeyPair_generate_key_should_create_two_files_private_and_public_key()
        {
            var rsa = new RsaAsymmetricCryptoProvider();
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string priKey = Path.Combine(wkFolder, "GenerateKeyPair_generate_key"+"_private.key");
            string pubKey = Path.Combine(wkFolder, "GenerateKeyPair_generate_key" + "_public.key");
            rsa.GenerateKeyPair(2048, "GenerateKeyPair_generate_key", wkFolder);

            Assert.IsTrue(File.Exists(priKey));
            Assert.IsTrue(File.Exists(pubKey));
        }

        [Test]
        public void Sign_and_verify_should_sign_with_valid_key_and_verify()
        {
            var rsa = new RsaAsymmetricCryptoProvider();
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string rsaPrivateKey = @"<RSAKeyValue><Modulus>uaMZVlpyvS1lhaYk3l0ExV7ilKtN2aTTpUOZ364XspLO8RZj3U9gdDUbuA8YyM2iDPnscCSz0hqoU00xTZiNGOZQVaMltD0OfOqRt364kgCD/s2AhpCM9XLHEqiaggOWYfJPwf8XHsMy5DUQGr1xFmwk3FDxq3hMpHFeUF1sdZsL/LVI3REFZYlTNci6I2Ze3k4/THuQlUfDO2gdGk5eYNlmfQ154WzdTI8m+8HVwAzRf/kHvGWcBJgPThGEHzMEgLoiroxfux/lNHfyO5JRzkb9Qx2CFFT15yGizz1JYFvKZQJEb43gME7YRyBXj9n6bDzrdPsClJGR+dgEAAh/0w==</Modulus><Exponent>AQAB</Exponent><P>9+VzvrNVCWCGihKiRAG1VmwQ10qlfcnJSyA8k+rHGb1X1S1rBjiqtwKGuUMS0iTbbLssW0YpzXqYa7PN/vvYExfpbcQhx+4MXcKp/U0giKq//J0HsXRmUOMDOumW//nFfbmUK/igRfUOHU3UfAfMz1dwmh1eRx2v6ZS8ILPQkGk=</P><Q>v7Sfs99Ob3VsCbwsfAF/e1J9Xc6r28ljv6q/SPDQyFozUGgyQEvMz+MV9zchy1LaxTxeJ2NP93oPtUesFWjcju8ebHlmnmHambwhVuW/Jjl4DvWKc6ZUM2vOBsis+h+W6Q1mUFbTbGCXEYpHMPOwaXSM4Rv726BDOxH2NtgWhts=</Q><DP>dSgW3LCqZsUcsJJ/574a8p4eE3Gu5tT/8iZpGsshj7OhmBlf9E/Btr5V56agdXp/zVJkczqktPzUnkoa+AAULWnmoz7HbFK/u2wtpI3X618vXvbj1OUbGe0/8I7HE3D6+iFDushFDury4byPyJJzJCbCC8QHc3q/UZg+lQIGdsk=</DP><DQ>c74J7f+Uy5aJdBc4hGYjIcHcDxcIi9o7by0X8GH0rAJJmPJP5KHfNmRUjr2qmaecjhi0f/NmBHSdp0VukD6Pa3zTUYq6ekV6Rfdf6acskVeBNQbqYIi7rZAZA9+FED6iTnLYowjI/VYT33MtKFD44bKMnBzZIvDPpophrRsp/qE=</DQ><InverseQ>XC8ltBkEocB7X8IyHYfpPP8wV4B43BnWbwVeucRSb3VA6dZkC8UubOC6XbDFMfe9Xuer1sIP4L4TNbf04hHZjfuyNACt07HT2uJkXdDLVtnm+Bhczl4WLX9qlaF6fyoEFAusXMjTYx2QkcrGt8hGzRUN7XKZvuTH34vA7nsMU1g=</InverseQ><D>BlVmO+gCGio+WHz+rQeHbiR6no/prA9PmUfhLByIenM/1EHrTlWy+MQHGhSJx/AcI00zn+/gor5+F7l+gSM/TSYuf319f9TFdcWbCyOrsG+MEVl5lR3BcW5lFGHkQ6NTsivORNQrOcV3KwoUbpUgEp/F8i5g0HTeSI4FpcBKPwjoNCVJZmiubwe5qA3HVunZ4sdukahTECNRP6Sx3HHlc6rT1BGj7vv/4qtLF8h1ryIld0YuDCM3xzdsbiz0huxg9735csGr9NiQY65sBefyXwH7hQZ8OZfnYEb9yTtIVSfJgk7JqS67ecZoHX2PRFiV0p3XenFWsb/eAwQ6CgxJwQ==</D></RSAKeyValue>";
            string rsaPublicKey = @"<RSAKeyValue><Modulus>uaMZVlpyvS1lhaYk3l0ExV7ilKtN2aTTpUOZ364XspLO8RZj3U9gdDUbuA8YyM2iDPnscCSz0hqoU00xTZiNGOZQVaMltD0OfOqRt364kgCD/s2AhpCM9XLHEqiaggOWYfJPwf8XHsMy5DUQGr1xFmwk3FDxq3hMpHFeUF1sdZsL/LVI3REFZYlTNci6I2Ze3k4/THuQlUfDO2gdGk5eYNlmfQ154WzdTI8m+8HVwAzRf/kHvGWcBJgPThGEHzMEgLoiroxfux/lNHfyO5JRzkb9Qx2CFFT15yGizz1JYFvKZQJEb43gME7YRyBXj9n6bDzrdPsClJGR+dgEAAh/0w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var signedBites = rsa.Sign(rsaPrivateKey, wkFolder);
            Assert.IsNotEmpty(signedBites);
            Assert.IsTrue(rsa.Verify(rsaPublicKey, signedBites, wkFolder.ConvertToByteArray()));
        }

        [Test]
        public void encrypt_decrypt_should_not_fail()
        {
            var test = "Une idée qu’il a eu à émettre lors du récent forum d’Addis-Abeba autour de la problématique.  Au sujet du contrôle de l’exécutif, des entreprises et services public.";
            var rsa = new RsaAsymmetricCryptoProvider();
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string rsaPrivateKey = @"<RSAKeyValue><Modulus>vTRSsteV+BZ2Vgs/wSj2LGOrJMNWV91B8lBxN6/D1/kizFai+wT1fZwQfxt4bKFI81XP6BZifAY6tJlE8cOHOhbsQ1X8c37/l3vwhf5VLa9xoco4i7JdEHiHYPbAYlWhATasV5Xn2o1pR3lsXBn+0KEsp5h0pLAL1S0Bo8NDCPFHNwAVAk/XykqKXoqXkLf1RmzPjvCG1GILShvTOx/69HQIJLt9UF3ocl7iPTh08XSN7SEcXjesrVRrO4kmaEpnTXcKltgoXGBI8bZns+9HiBWHtPTGWPvqFDrJL98AhoOjDHwJhKGCyenLUoK5A/oK7j25eS4GOseTopPZqK1gSQ==</Modulus><Exponent>AQAB</Exponent><P>4KeBIxoHoPM1A7Jg4piIwsap982/jRUVaEzbYENavK+SVNaa9qHkQ7HY5tT8+JhY4aiS+LoFqogx7W1Vhox5dlPip4p4f5cgF2xpLHPt5F4Xm7lmImmmWyFOvZ87nfl+nfTd8LAKxLkv39X3qaj34sTP6ZP73Wdb+3R6094llWs=</P><Q>15qSZ922sYcwi9d4qApgvRA0Ke0cBaIvyQhyPWogELDjokNakAfc9PhnEHTjfyz0jUG70vjt4LvUG+ucOmiv8XUTPpTPosfYKponXaNuoleQPTUel4aaYD5lz1kOpYoohDjosiw7ylyspwO9aY8eoVXXxsprqrcdyxcnj/lNWhs=</Q><DP>WfR4jxejKl14QuywQsuVJ3jpIiKuqs1gbw0nYppVwOwEihgMoOkeP8T89yEd0mUeYe1gPFwwXKKAa3O2JVQmZq0cOr4FlgFrhjWQv44EGcdbaK2KKgln5WGm5+LUumLmwlUEcZXsGe2VL+m7a6IO8CyinAL3fSYf/MYdUKzJG9U=</DP><DQ>Szb6Yk+/n1kpP2/RANZ1/x77A7FbOD/nabuPlwtB2nDLSN00Z9DGbZOG6P/OKZy2R90puCTtX5xHF+JvIxVIY/TS3N8vPHt5VwtmNk2AmBPyFthELtpfWroJ7HcGHAwBrHbp/tZdmXARL0anun6aUfBPkWMmgmzVcPdH7vsq2Ac=</DQ><InverseQ>vsG6eIdM/k7XMv9uVVHXaiDHkDgw9p63veRrOUBN5J07htnUVxy8oxZ6fpO0V8VbUdShBhimsb8ll7iozHSxC6xFQcQKlyf6uaAZcRDBfKmWZL4aIY5pylW3vP5G6xTAUXdkueTEWnyJZbmvI6g8spnXBpSpSI9SjcgIZgJq8HI=</InverseQ><D>OtzdYaBqms06MZi8U/bPZ432f7B520oNqzpVjtKS5k0vA01s1RV83oBALZs5QuqGjJXE4dqc9yLk/qy0Y1bGak8ZX/WPXctD7zfy42z1yQnNG12Ta/qos2gcDhGWVsF3Hq6IvZL4l+jouQDQsnKv8O5DuzxWBapzc/XVjcJAx7XnztEMPS96vxlo4+TVNeLGbFeCQvnl3GzObjf9Nz0NSf6ABM8D5l/W3DaqN9zG+gaNwk32JOJEVsO49krNmBzLAck/tCbNhkHn7R5RrNWHQbA38KqJYcfiRu2ALHaMuygou6DJ+FSbfwelpsOr8huuJYOG/5qDP4EA7kNeuSIqVQ==</D></RSAKeyValue>";
            string rsaPublicKey = @"<RSAKeyValue><Modulus>vTRSsteV+BZ2Vgs/wSj2LGOrJMNWV91B8lBxN6/D1/kizFai+wT1fZwQfxt4bKFI81XP6BZifAY6tJlE8cOHOhbsQ1X8c37/l3vwhf5VLa9xoco4i7JdEHiHYPbAYlWhATasV5Xn2o1pR3lsXBn+0KEsp5h0pLAL1S0Bo8NDCPFHNwAVAk/XykqKXoqXkLf1RmzPjvCG1GILShvTOx/69HQIJLt9UF3ocl7iPTh08XSN7SEcXjesrVRrO4kmaEpnTXcKltgoXGBI8bZns+9HiBWHtPTGWPvqFDrJL98AhoOjDHwJhKGCyenLUoK5A/oK7j25eS4GOseTopPZqK1gSQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var armoredBytes = rsa.Encrypt(rsaPublicKey, test.ConvertToByteArray());
            Assert.IsNotEmpty(armoredBytes);
            var clearBytes = rsa.Decrypt(rsaPrivateKey, armoredBytes);
            Assert.AreEqual(test, clearBytes.ConvertToString());
        }
    }
}
