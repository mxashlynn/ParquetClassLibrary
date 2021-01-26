using System;

namespace Parquet
{
    /// <summary>
    /// Represents a type used to perform logging.
    /// </summary>
    /// <remarks>
    /// Inspired by <see cref="Microsoft.Extensions.Logging.ILogger"/> but simpler.
    /// </remarks>
    public interface ILogger
    {
        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <param name="inLogLevel">The severity of the event being logged.</param>
        /// <param name="inMessage">A message summarizing the event being logged.</param>
        /// <param name="inException">The exception related to this event, if any.</param>
        void Log(LogLevel inLogLevel, string inMessage, Exception inException);
    }
}
