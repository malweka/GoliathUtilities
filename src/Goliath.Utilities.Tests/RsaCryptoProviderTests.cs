using System;
using System.IO;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class RsaCryptoProviderTests
    {
        [TestMethod]
        public void GenerateKeyPair_generate_key_should_create_two_files_private_and_public_key()
        {
            var rsa = new RsaXmlCryptoProvider();
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string priKey = Path.Combine(wkFolder, "GenerateKeyPair_generate_key"+"_private.key");
            string pubKey = Path.Combine(wkFolder, "GenerateKeyPair_generate_key" + "_public.key");
            rsa.GenerateKeyPair(2048, "GenerateKeyPair_generate_key", wkFolder);

            Assert.IsTrue(File.Exists(priKey));
            Assert.IsTrue(File.Exists(pubKey));
        }

        [TestMethod]
        public void Sign_and_verify_should_sign_with_valid_key_and_verify()
        {
            var rsa = new RsaXmlCryptoProvider();
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string rsaPrivateKey = @"<RSAKeyValue><Modulus>l7EzMLcKyCWRJGx0llt7ydar6e+ylPvGjmK5NiQ4kNMxfej1KKMamBkuOuNiCoQIivIUsktQ9E+FqGk+oQZN0qx72nzCLVt/WjsZkYxN493tq5phYypZAisGWQIQz2fdpCD66nrZ5a3s7IEZp26iomOt+OZ/BERz/yPDuMgy3YuG2WYmlINR+JCfRKROKCcUw7DwjNnzXSNcx3CvryDvAkQgPZAlu2OihcCR/Ayb7uVDh0GdQ5AFVxKffzHdmG9s0WB86vrpmgnjGV54FMQQpAQ9mN3iO3ItAVudv/S00d9NJp18x4OK6OK/aO1kWqGf5WQmrFUDO9EgO7T8YjlLhw==</Modulus><Exponent>AQAB</Exponent><P>0DN4Etsb4XjIvTjv/GeN1KkJ2hzvSJ1fPAvWOXwAeG7kp275/58SKanLRroSt3s1isJmJ2O+f+9257Voijz0A3b+uG3N1g36DP7ejXQXMrojjrw03JlgyjCmiVw7NDLSxmrR6cmrPOVIXb2u9edaw6ZBH0rx+UhwSYEyv4PfhQU=</P><Q>uoSN6BaOiN6g4+FHRbkfeEodZFMQPkM2oTshmwLxzHIaE1r9GT8gOTc2ukKdooHr3T5IuW9pBNasV2WXIsdahCEgMwoT3WPvYdk7uh2AL5U2CRfiXO1/5gDuZNAjRYPF+u3y/l/IqoMc2Mb+34/QBbn0MSefkV+jSgc/bingdBs=</Q><DP>KQNajexXq6zNbmj+7WvAxrmd1TeJ83X8wqlaQ+yncxH1PP5hhPdi7o4iGwaglUBSJclxsiS17CHR/IcB1ul28A/K0a3ftGEAdvrmAFt0DmwYgQ+WIjacmHfI9poSl4/DcY6tVy6A6vgHr1+kTZCLkqr3fSCYVhDs/a3Tv2JM77k=</DP><DQ>boVxWR9UooHZMG8jPottvGeedv5JV7uYOX3CcgoSoaTTErkN1NH1FDJFeaVTpyH5U4Y+rbL4tedHBBqsre0XE8hVPikwkDVRedexbp1ggdGWzVibx5jr+qx+lrpGzEBDJmetX1H+pBtBEqsICA09pMcFjcL+6LOVHJ6i1XJ3EBU=</DQ><InverseQ>vYcxbFvwDKRlANH1Il2mlkzIjN2yaoVqBWqEv1gnBRsywVQhT22PRh4ZgnQOcrl9mvRQdNHdc9GVSCvDt285x4i9SB1odY5LEqTttUoZADFJXEM/YuJ6vYL/Qh2JNtjt2KVYud3+DK0xW2pN2jaI9J6IQUeorI7p1uG/lMT0zx8=</InverseQ><D>GbcafHmrRD7KGiigoxSjKZZQ0nmmBoegI2ctCradOD/1NekWFmuACKTMJ4OAjVPQtu1PAOKvuJr6h5A/48BT1REUdfeMW3AVaNB4ByqH0cc/kUW8mLkHGcz9aH1nKCHtevN0Vee5pKwVbAp+tNmWjHzlczpv8eNA3tHLTjnyFYhcy7On8hVfPvyDPeH6lhHZA8sz/Jwyo/LA2KCkV8ixYq6HAk/B1IWSynTb085qB3gycN/TPiMZGHjcGGBBx/rDoS8yXxa5DZhi3OWYa8RZUVUUgQVEeEfppreB8D0ZjAgrjxvWJe10xEWDvV3g9UKiHzXPbz9p43HQtlru+71NcQ==</D></RSAKeyValue>";
            string rsaPublicKey = @"<RSAKeyValue><Modulus>l7EzMLcKyCWRJGx0llt7ydar6e+ylPvGjmK5NiQ4kNMxfej1KKMamBkuOuNiCoQIivIUsktQ9E+FqGk+oQZN0qx72nzCLVt/WjsZkYxN493tq5phYypZAisGWQIQz2fdpCD66nrZ5a3s7IEZp26iomOt+OZ/BERz/yPDuMgy3YuG2WYmlINR+JCfRKROKCcUw7DwjNnzXSNcx3CvryDvAkQgPZAlu2OihcCR/Ayb7uVDh0GdQ5AFVxKffzHdmG9s0WB86vrpmgnjGV54FMQQpAQ9mN3iO3ItAVudv/S00d9NJp18x4OK6OK/aO1kWqGf5WQmrFUDO9EgO7T8YjlLhw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var signedBites = rsa.Sign(rsaPrivateKey, wkFolder);
            Assert.IsNotNull(signedBites);
            Assert.IsTrue(rsa.Verify(rsaPublicKey, signedBites, wkFolder.ConvertToByteArray()));
        }

        [TestMethod]
        public void encrypt_decrypt_should_not_fail()
        {
            var test = "Une idée qu’il a eu à émettre lors du récent forum d’Addis-Abeba autour de la problématique.  Au sujet du contrôle de l’exécutif, des entreprises et services public.";
            var rsa = new RsaXmlCryptoProvider();
            var wkFolder = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string rsaPrivateKey = @"<RSAKeyValue><Modulus>vTRSsteV+BZ2Vgs/wSj2LGOrJMNWV91B8lBxN6/D1/kizFai+wT1fZwQfxt4bKFI81XP6BZifAY6tJlE8cOHOhbsQ1X8c37/l3vwhf5VLa9xoco4i7JdEHiHYPbAYlWhATasV5Xn2o1pR3lsXBn+0KEsp5h0pLAL1S0Bo8NDCPFHNwAVAk/XykqKXoqXkLf1RmzPjvCG1GILShvTOx/69HQIJLt9UF3ocl7iPTh08XSN7SEcXjesrVRrO4kmaEpnTXcKltgoXGBI8bZns+9HiBWHtPTGWPvqFDrJL98AhoOjDHwJhKGCyenLUoK5A/oK7j25eS4GOseTopPZqK1gSQ==</Modulus><Exponent>AQAB</Exponent><P>4KeBIxoHoPM1A7Jg4piIwsap982/jRUVaEzbYENavK+SVNaa9qHkQ7HY5tT8+JhY4aiS+LoFqogx7W1Vhox5dlPip4p4f5cgF2xpLHPt5F4Xm7lmImmmWyFOvZ87nfl+nfTd8LAKxLkv39X3qaj34sTP6ZP73Wdb+3R6094llWs=</P><Q>15qSZ922sYcwi9d4qApgvRA0Ke0cBaIvyQhyPWogELDjokNakAfc9PhnEHTjfyz0jUG70vjt4LvUG+ucOmiv8XUTPpTPosfYKponXaNuoleQPTUel4aaYD5lz1kOpYoohDjosiw7ylyspwO9aY8eoVXXxsprqrcdyxcnj/lNWhs=</Q><DP>WfR4jxejKl14QuywQsuVJ3jpIiKuqs1gbw0nYppVwOwEihgMoOkeP8T89yEd0mUeYe1gPFwwXKKAa3O2JVQmZq0cOr4FlgFrhjWQv44EGcdbaK2KKgln5WGm5+LUumLmwlUEcZXsGe2VL+m7a6IO8CyinAL3fSYf/MYdUKzJG9U=</DP><DQ>Szb6Yk+/n1kpP2/RANZ1/x77A7FbOD/nabuPlwtB2nDLSN00Z9DGbZOG6P/OKZy2R90puCTtX5xHF+JvIxVIY/TS3N8vPHt5VwtmNk2AmBPyFthELtpfWroJ7HcGHAwBrHbp/tZdmXARL0anun6aUfBPkWMmgmzVcPdH7vsq2Ac=</DQ><InverseQ>vsG6eIdM/k7XMv9uVVHXaiDHkDgw9p63veRrOUBN5J07htnUVxy8oxZ6fpO0V8VbUdShBhimsb8ll7iozHSxC6xFQcQKlyf6uaAZcRDBfKmWZL4aIY5pylW3vP5G6xTAUXdkueTEWnyJZbmvI6g8spnXBpSpSI9SjcgIZgJq8HI=</InverseQ><D>OtzdYaBqms06MZi8U/bPZ432f7B520oNqzpVjtKS5k0vA01s1RV83oBALZs5QuqGjJXE4dqc9yLk/qy0Y1bGak8ZX/WPXctD7zfy42z1yQnNG12Ta/qos2gcDhGWVsF3Hq6IvZL4l+jouQDQsnKv8O5DuzxWBapzc/XVjcJAx7XnztEMPS96vxlo4+TVNeLGbFeCQvnl3GzObjf9Nz0NSf6ABM8D5l/W3DaqN9zG+gaNwk32JOJEVsO49krNmBzLAck/tCbNhkHn7R5RrNWHQbA38KqJYcfiRu2ALHaMuygou6DJ+FSbfwelpsOr8huuJYOG/5qDP4EA7kNeuSIqVQ==</D></RSAKeyValue>";
            string rsaPublicKey = @"<RSAKeyValue><Modulus>vTRSsteV+BZ2Vgs/wSj2LGOrJMNWV91B8lBxN6/D1/kizFai+wT1fZwQfxt4bKFI81XP6BZifAY6tJlE8cOHOhbsQ1X8c37/l3vwhf5VLa9xoco4i7JdEHiHYPbAYlWhATasV5Xn2o1pR3lsXBn+0KEsp5h0pLAL1S0Bo8NDCPFHNwAVAk/XykqKXoqXkLf1RmzPjvCG1GILShvTOx/69HQIJLt9UF3ocl7iPTh08XSN7SEcXjesrVRrO4kmaEpnTXcKltgoXGBI8bZns+9HiBWHtPTGWPvqFDrJL98AhoOjDHwJhKGCyenLUoK5A/oK7j25eS4GOseTopPZqK1gSQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var armoredBytes = rsa.Encrypt(rsaPublicKey, test.ConvertToByteArray());
            Assert.IsNotNull(armoredBytes);
            var clearBytes = rsa.Decrypt(rsaPrivateKey, armoredBytes);
            Assert.AreEqual(test, clearBytes.ConvertToString());
        }
    }
}
