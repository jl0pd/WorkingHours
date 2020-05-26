using System.Collections.Generic;
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
        }

        private IReadOnlyCollection<string> ThirdParty { get; } = new[]
        {
            "https://icons8.com"
        };

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
