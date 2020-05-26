using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    class SettingsPanelViewModel : ViewModelBase
    {
        public SettingsPanelViewModel() => Back = ReactiveCommand.Create(() => { });

        public ReactiveCommand<Unit, Unit> Back { get; }

        public void ShowAbout() => new AboutWindow().Show();
    }
}
