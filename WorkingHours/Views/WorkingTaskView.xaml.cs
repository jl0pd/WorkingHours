using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using WorkingHours.ViewModels;

namespace WorkingHours.Views
{
    public class WorkingTaskView : ReactiveUserControl<WorkingTaskViewModel>
    {
        public WorkingTaskView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
