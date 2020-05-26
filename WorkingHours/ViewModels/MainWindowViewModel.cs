using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using WorkingHours.Models;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Content = List = new WorkingTasksViewModel(Enumerable.Empty<WorkingTask>());
        }

        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public void ShowElapsed()
        {
            var vm = new TotalElapsedViewModel(List.Items.Select(t => t.Task));
            vm.Back.Take(1).Subscribe(u => Content = List);
            Content = vm;
        }

        public void ShowSettings()
        {
            var vm = new SettingsPanelViewModel();
            vm.Back.Take(1).Subscribe(_ => Content = List);
            Content = vm;
        }

        public void AddItem()
        {
            var vm = new AddTaskViewModel();

            Observable.Merge(vm.Add, vm.Cancel.Select(_ => (WorkingTask?)null))
                .Take(1)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        List.Items.Add(new WorkingTaskItemViewModel(item));
                        new MiniMainWindow(List.Items.Last()).Show();
                    }
                    Content = List;
                });
            Content = vm;

        }

        public void Pin()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow.Topmost = !desktop.MainWindow.Topmost;
            }
        }

        public WorkingTasksViewModel List { get; }

        private ViewModelBase _content = null!;
    }
}
