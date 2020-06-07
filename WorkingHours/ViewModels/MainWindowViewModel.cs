using System;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI.Fody.Helpers;
using WorkingHours.Models;
using WorkingHours.Utils;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Content = List = new WorkingTasksViewModel(Enumerable.Empty<WorkingTask>());
        }

        [Reactive] public ViewModelBase Content { get; set; }

        public void ShowElapsed()
        {
            var vm = new TotalElapsedViewModel(List.WorkingTaskItemViewModels.Items.Select(t => t.Task));
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

            Observable.Merge(vm.Add, vm.Cancel.Select<System.Reactive.Unit, WorkingTask?>(_ => null))
                .Take(1)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        var itemVm = new WorkingTaskItemViewModel(item);
                        List.WorkingTaskItemViewModels.Add(itemVm);
                        new MiniMainWindow(itemVm)
                        {
                            Owner = WindowingUtils.GetMainWindow() as Window // hope it will be fixed some day https://github.com/AvaloniaUI/Avalonia/issues/3254
                        }.Show();
                    }
                    Content = List;
                });
            Content = vm;
        }

        public void Pin()
        {
            Control? mainWindow = WindowingUtils.GetMainWindow();
            if (mainWindow is Window window)
            {
                window.Topmost = !window.Topmost;
            }
        }

        public WorkingTasksViewModel List { get; }
    }
}
