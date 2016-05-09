namespace Goliath.Web.Authorization
{
    public interface ICacheProvider
    {
        T Get<T>(string key);

        void Set<T>(string key, T value);

        bool Remove(string key);

        void ReleaseAll();
    }
}