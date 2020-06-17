using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using WorkingHours.Logging;
using WorkingHours.Logging.Serilog;
using WorkingHours.Providers;
using WorkingHours.ViewModels;
using WorkingHours.Views;

namespace WorkingHours
{
    public class App : Application
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            Log.Logger = new SerilogLogger();

            TaskScheduler.UnobservedTaskException += (sender, e) => OnUnhandledExceptionThrown(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => OnUnhandledExceptionThrown((Exception)e.ExceptionObject);

            DialogProvider.DialogService = new AvaloniaDialogService();
            var mainVM = new MainWindowViewModel(useDB: !Design.IsDesignMode);
            var mainWindow = new MainWindow
            {
                DataContext = mainVM,
            };

            switch (ApplicationLifetime)
            {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = mainWindow;
                desktop.Exit += (sender, e) => mainVM.Save();
                break;
            case ISingleViewApplicationLifetime singleView:
                singleView.MainView = mainWindow;
                break;
            default:
                Log.Warn("Unhandled lifetime {ApplicationLifetime}", ApplicationLifetime);
                break;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnUnhandledExceptionThrown(Exception? e) => Log.Fatal("{Exception}", e);
    }
}
