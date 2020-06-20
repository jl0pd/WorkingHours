using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WorkingHours.Views
{
    public class WorkingDayView : UserControl
    {
        public WorkingDayView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
