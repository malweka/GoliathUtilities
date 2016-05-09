using Goliath.Data;

namespace Goliath.Web.Authorization
{
    public interface IDatabaseProvider
    {
        ISessionFactory SessionFactory { get; }
        void Init();
    }
}