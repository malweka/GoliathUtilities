using System;

namespace Goliath.Models
{
    [Flags]
    public enum AuthenticationStatus
    {
        None = 0,
        Authenticated = 1,
        Denied = 2,
        PasswordExpired = 4,
        Disabled = 8,
        LockedAccount = 16,
        InvalidAccount = 32
    }
}
