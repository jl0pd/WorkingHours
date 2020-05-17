using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WorkingHours.Views
{
    public class AddTaskView : UserControl
    {
        public AddTaskView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
