using System.Reactive;
using ReactiveUI;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    internal class SettingsPanelViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> Back { get; } = ReactiveCommand.Create(() => { });

        public void ShowAbout() => new AboutWindow().Show();
    }
}
