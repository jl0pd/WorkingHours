using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace WorkingHours.Utils
{
    internal static class WindowingUtils
    {
        public static Control? GetMainWindow() => Application.Current.ApplicationLifetime switch
        {
            IClassicDesktopStyleApplicationLifetime desktop => desktop.MainWindow,
            ISingleViewApplicationLifetime singleView => singleView.MainView,
            _ => null,
        };
    }
}
