using Goliath.Data;

namespace Goliath.Authorization
{
    public interface IDatabaseProvider
    {
        ISessionFactory SessionFactory { get; }
        void Init();
    }
}