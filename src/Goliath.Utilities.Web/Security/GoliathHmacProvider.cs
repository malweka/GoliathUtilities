namespace Goliath.Security
{
    public class GoliathHmacProvider : Nancy.Cryptography.IHmacProvider
    {
        private readonly IHmacProvider provider;
        private readonly byte[] secretByte;

        public GoliathHmacProvider(IHmacProvider provider, int length, string secret)
        {
            this.provider = provider;
            HmacLength = length;
            secretByte = secret.ConvertToByteArray();
        }

        public byte[] GenerateHmac(string data)
        {
            var dataByte = data.ConvertToByteArray();
            return GenerateHmac(dataByte);
        }

        public byte[] GenerateHmac(byte[] data)
        {
            return provider.ComputeHash(secretByte, data);
        }

        public int HmacLength { get; private set; }
    }
}