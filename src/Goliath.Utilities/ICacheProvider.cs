using System;
using System.Threading.Tasks;

namespace Goliath.Data
{
    public interface ICacheProvider
    {
        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        bool Add<T>(string key, T value, TimeSpan? expiry = null);

        Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiry = null);

        bool ContainsKey(string key);

        bool TryGetValue<T>(string key, out T value);

        bool Remove(string key);

        void ReleaseAll();
    }
}