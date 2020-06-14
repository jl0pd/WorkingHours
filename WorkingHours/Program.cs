using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using Serilog;

namespace WorkingHours
{
    static class Program
    {
        public static AppBuilder ConfigureLogging(this AppBuilder builder)
        {
            string template = "{Timestamp:yyyy.MM.dd HH:mm:ss} | {Level:u5} | {Message:lj}{NewLine}{Exception}";

            Log.Logger =
                new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.File("Logs/log-.log", outputTemplate: template, rollingInterval: RollingInterval.Day)
#if DEBUG
                    .WriteTo.Console(outputTemplate: template)
                    .WriteTo.Debug(outputTemplate: template)
#endif
                    .CreateLogger();

            return builder;
        }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => 
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .LogToTrace()
                .ConfigureLogging()
                .UsePlatformDetect()
                .LogToDebug()
                .UseReactiveUI();
    }
}
