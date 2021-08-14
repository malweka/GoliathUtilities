using System;

namespace Goliath
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets the current level.
        /// </summary>
        /// <value>The current level.</value>
        LogLevel CurrentLevel { get; }

        /// <summary>
        /// Logs the specified log type.
        /// </summary>
        /// <param name="logLevel">Type of the log.</param>
        /// <param name="message">The message.</param>
        /// <param name="sessionId">The session id.</param>
        void Log(LogLevel logLevel, string message, long? sessionId = null);

        /// <summary>
        /// Logs the specified log type.
        /// </summary>
        /// <param name="logLevel">Type of the log.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception"></param>
        /// <param name="sessionId">The session id.</param>
        void Log(LogLevel logLevel, string message, Exception exception, long? sessionId = null);

    }

    /// <summary>
    /// 
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 
        /// </summary>
        Info = 2,

        /// <summary>
        /// 
        /// </summary>
        Warning = 4,
        /// <summary>
        /// 
        /// </summary>
        Error = 8,
        /// <summary>
        /// 
        /// </summary>
        Fatal = 64,
        /// <summary>
        /// 
        /// </summary>
        All = Debug
    }

}
