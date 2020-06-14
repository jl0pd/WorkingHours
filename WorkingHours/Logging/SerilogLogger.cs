using SLog = Serilog.Log;

namespace WorkingHours.Logging
{
    internal class SerilogLogger : ILogger
    {
        public void Debug<T>(string template, T value) => SLog.Debug(template, value);
        public void Error<T>(string template, T value) => SLog.Error(template, value);
        public void Info<T>(string template, T value) => SLog.Information(template, value);
        public void Warn<T>(string template, T value) => SLog.Warning(template, value);
        public void Fatal<T>(string template, T value) => SLog.Fatal(template, value);
        public void Trace<T>(string template, T value) => SLog.Verbose(template, value);
    }
}
