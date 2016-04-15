//using System;

using Goliath.Web;

namespace Goliath.Authorization
{
    public interface IAppService
    {
        ApplicationContext CurrentContext { get; }
    }
}