#nullable disable

namespace WorkingHours.Logging
{
    internal static class Log
    {
        public static ILogger Logger { get; set; }

        public static void Debug<T>(string template, T value) => Logger.Debug(template, value);
        public static void Error<T>(string template, T value) => Logger.Error(template, value);
        public static void Fatal<T>(string template, T value) => Logger.Fatal(template, value);
        public static void Info<T>(string template, T value) => Logger.Info(template, value);
        public static void Trace<T>(string template, T value) => Logger.Trace(template, value);
        public static void Warn<T>(string template, T value) => Logger.Warn(template, value);
    }
}
