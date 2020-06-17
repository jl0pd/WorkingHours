using System.Collections.Generic;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WorkingHours.Views
{
    public class AboutWindow : Window
    {
        public AboutWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var lb = this.FindControl<ListBox>("ThirdPartyListBox");
            lb.Items = ThirdParty;

            var tb = this.FindControl<TextBox>("VersionTextBox");
            tb.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "";
        }

        private IReadOnlyCollection<string> ThirdParty { get; } = new[]
        {
            "Icons from https://icons8.com"
        };

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
