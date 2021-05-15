using System;

namespace Goliath.Models
{
    [Flags]
    public enum AuthenticationStatus
    {
        None = 0,
        Failed = 1,
        Success = 2,
        PasswordExpired = 4,
        Disabled = 8
    }
}
