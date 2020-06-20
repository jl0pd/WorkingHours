using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WorkingHours.Views
{
    public class CurrentDayEditorView : UserControl
    {
        public CurrentDayEditorView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
