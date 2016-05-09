//using System;

namespace Goliath.Web.Authorization
{
    public interface IAppService
    {
        ApplicationContext CurrentContext { get; }
    }
}