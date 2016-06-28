using System;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum ViewActionType
    {
        /// <summary>
        /// The none
        /// </summary>
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

    //[Flags]
    //public enum PermActionType
    //{
    //    None = 0,
    //    View = 1,
    //    Edit = 2,
    //    Create = 4,
    //    Delete = 8,
    //    List = 16
    //}


    public enum VisualEditorType
    {
        TextBox,
        Editor,
        CheckBox,
        DatePicker,
    }


    [Flags]
    public enum AuthenticationStatus
    {
        None = 0,
        Failed = 1,
        Success = 2,
        PasswordExpired = 4,
        Disabled = 8
    }

    //public enum CacheServiceType
    //{
    //    InMemory = 0,
    //    Redis,
    //    Memcache
    //}
}
