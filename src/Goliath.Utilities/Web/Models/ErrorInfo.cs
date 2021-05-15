using System;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct ErrorInfo
    {
        /// <summary>
        /// The control identifier
        /// </summary>
        public string ControlId;

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage;
    }
}