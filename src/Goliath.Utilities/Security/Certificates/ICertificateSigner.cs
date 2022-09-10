namespace Goliath.Security
{
    interface ICertificateSigner
    {
        byte[] SignData(byte[] data);
        bool Verify(byte[] signature, byte[] data);
    }
}