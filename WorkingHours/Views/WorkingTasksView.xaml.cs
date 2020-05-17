using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WorkingHours.Views
{
    public class WorkingTasksView : UserControl
    {
        public WorkingTasksView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
