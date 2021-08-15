using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goliath.Security
{
    public class SecurityServiceFactory : ISecurityServiceFactory
    {
        private readonly Dictionary<string, IHashProvider> hashProviders;
        private readonly Dictionary<string, ISymmetricCryptoProvider> symmetricCryptos;
        private readonly Dictionary<string, IAsymmetricCryptoProvider> asymmetricCryptos;

        public SecurityServiceFactory(IEnumerable<IHashProvider> hashProviders, 
            IEnumerable<ISymmetricCryptoProvider> symmetricCryptoProviders,
            IEnumerable<IAsymmetricCryptoProvider> asymmetricCryptoProviders)
        {
            if (hashProviders == null) throw new ArgumentNullException(nameof(hashProviders));
            if (symmetricCryptoProviders == null) throw new ArgumentNullException(nameof(symmetricCryptoProviders));

            this.hashProviders = hashProviders.ToDictionary(c => c.Name);
            symmetricCryptos = symmetricCryptoProviders.ToDictionary(c => c.Name);
            asymmetricCryptos = asymmetricCryptoProviders.ToDictionary(c => c.Name);
        }

        public IHashProvider GetHashProvider(string providerName)
        {
            if (!hashProviders.TryGetValue(providerName, out IHashProvider hashProvider))
                throw new InvalidOperationException($"Hash provider {providerName} does not exist.");
            return hashProvider;
        }

        public ISymmetricCryptoProvider GetSymmetricCryptoProvider(string providerName)
        {
            if (!symmetricCryptos.TryGetValue(providerName, out ISymmetricCryptoProvider provider))
                throw new InvalidOperationException($"provider {providerName} does not exist.");
            return provider;
        }

        public IAsymmetricCryptoProvider GetAsymmetricCryptoProvider(string providerName)
        {
            if (!asymmetricCryptos.TryGetValue(providerName, out IAsymmetricCryptoProvider provider))
                throw new InvalidOperationException($"provider {providerName} does not exist.");
            return provider;
        }
    }


}
