//using System;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppService
    {
        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>
        /// The current context.
        /// </value>
        ApplicationContext CurrentContext { get; }
    }
}