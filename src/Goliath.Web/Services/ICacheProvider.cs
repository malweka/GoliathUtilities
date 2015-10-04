namespace Goliath.Web.Services
{
    public interface ICacheProvider
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);

        bool Remove(string key);
    }
}