using System.Collections.Generic;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppUser: Microsoft.AspNet.Identity.IUser<long>
    {
        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        IDictionary<string, IRole> Roles { get; }

    }

    public interface IPermissionAction
    {
        long Id { get; }
        string Name { get; }
        int PermValue { get; }
    }

}