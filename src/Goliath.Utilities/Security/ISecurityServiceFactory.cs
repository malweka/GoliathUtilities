namespace Goliath.Security
{
    public interface ISecurityServiceFactory
    {
        IHashProvider GetHashProvider(string providerName);
        ISymmetricCryptoProvider GetSymmetricCryptoProvider(string providerName);
        IAsymmetricCryptoProvider GetAsymmetricCryptoProvider(string providerName);
    }
}