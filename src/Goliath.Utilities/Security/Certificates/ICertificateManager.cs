namespace Goliath.Security
{
    public interface ICertificateManager
    {
        string SigningAlgorithmName { get; }
        byte[] SignData(byte[] data);
        bool VerifySignature(byte[] signature, byte[] data);
    }
}