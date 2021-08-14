using System;

namespace Goliath
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Logger : ILogger
    {
        readonly LogLevel currentLogLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        protected Logger(LogLevel logLevel = LogLevel.Debug)
        {
            CurrentLevel = logLevel;
            currentLogLevel = logLevel;
        }

        /// <summary>
        /// Determines whether this instance can log the specified log type.
        /// </summary>
        /// <param name="logLevel">Type of the log.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can log the specified log type; otherwise, <c>false</c>.
        /// </returns>
        protected bool CanLog(LogLevel logLevel)
        {
            if (logLevel >= currentLogLevel)
            {
                return true;
            }

            return false;
        }

        #region ILogger Members

        /// <inheritdoc/>
        public LogLevel CurrentLevel { get; private set; }

        /// <inheritdoc/>
        public abstract void Log(LogLevel logLevel, string message, long? sessionId = null);

        /// <inheritdoc/>
        public abstract void Log(LogLevel logLevel, string message, Exception exception, long? sessionId = null);

        #endregion

        static Func<string, ILogger> createLoggerMethod;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ILogger GetLogger(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return GetLogger(type.FullName);
        }

        public static ILogger GetLogger(string loggerName)
        {
            return createLoggerMethod == null ? new NullLogger() : createLoggerMethod(loggerName);
        }

        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="factoryMethod">The factory method.</param>
        public static void RegisterCurrentLogger(Func<string, ILogger> factoryMethod)
        {
            createLoggerMethod ??= factoryMethod;
        }
    }
}
