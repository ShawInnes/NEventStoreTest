using NEventStore.Logging;
using Serilog;

namespace NEventSourceTests.Infrastructure
{
    public class SerilogLogger : ILog
    {
        public void Verbose(string message, params object[] values)
        {
            Log.Verbose(message, values);
        }

        public void Debug(string message, params object[] values)
        {
            Log.Debug(message, values);
        }

        public void Info(string message, params object[] values)
        {
            Log.Information(message, values);
        }

        public void Warn(string message, params object[] values)
        {
            Log.Warning(message, values);
        }

        public void Error(string message, params object[] values)
        {
            Log.Error(message, values);
        }

        public void Fatal(string message, params object[] values)
        {
            Log.Fatal(message, values);
        }
    }
}