using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Serilog;
using WorkingHours.Providers;
using WorkingHours.ViewModels;
using WorkingHours.Views;

namespace WorkingHours
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) => OnUnhandledExceptionThrown(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => OnUnhandledExceptionThrown((Exception)e.ExceptionObject);

            DialogProvider.DialogService = new AvaloniaDialogService();

            var mainVM = new MainWindowViewModel(useDB: true);
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
                Debug.Write(ApplicationLifetime);
                break;
            }


            base.OnFrameworkInitializationCompleted();
        }

        private void OnUnhandledExceptionThrown(Exception? e) => Log.Error($"Exception {e}");
    }
}
