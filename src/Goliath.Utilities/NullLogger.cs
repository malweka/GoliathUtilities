using System;

namespace Goliath
{
    public class NullLogger : Logger
    {
        public NullLogger() : base()
        {
        }

        public override void Log(LogLevel logLevel, string message, long? sessionId = null)
        {

        }

        public override void Log(LogLevel logLevel, string message, Exception exception, long? sessionId = null)
        {

        }
    }
}