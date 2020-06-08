﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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
            var MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            switch (ApplicationLifetime)
            {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = MainWindow;
                break;
            case ISingleViewApplicationLifetime singleView:
                singleView.MainView = MainWindow;
                break;
            default:
                Debug.Write(ApplicationLifetime);
                break;
            }

            TaskScheduler.UnobservedTaskException += (sender, e) => OnUnhandledExceptionThrown(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => OnUnhandledExceptionThrown((Exception)e.ExceptionObject);

            DialogProvider.DialogService = new AvaloniaDialogService();

            base.OnFrameworkInitializationCompleted();
        }

        private void OnUnhandledExceptionThrown(Exception? e) => Debug.WriteLine(e);
    }
}
