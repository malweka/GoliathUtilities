namespace Goliath.Security
{
    public interface IHmacProvider
    {
        byte[] ComputeHash(byte[] secret, byte[] data);

       bool VerifyHash(byte[] secret, byte[] data, byte[] hash);
    }
}