using System;

namespace Goliath.Web
{
    public enum ViewActionType
    {
        None = 0,
        Add,
        Edit,
        List,
        Delete
    }

    public enum ViewExecutionState
    {
        None = 0,
        HasError,
        HasWarning,
        HasInfo,
        Success,
    }

    [Flags]
    public enum 
        PermActionType
    {
        None =0,
        View = 1,
        Edit = 2,
        Create = 4,
        Delete = 8,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// The viewer
        /// </summary>
        Viewer = 0,

        /// <summary>
        /// The editor
        /// </summary>
        Editor = 1,

        /// <summary>
        /// The admin
        /// </summary>
        Admin = 2,

        /// <summary>
        /// The site admin
        /// </summary>
        SiteAdmin = 4,
    }

   

}
