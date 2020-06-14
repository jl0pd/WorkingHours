using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using ReactiveUI.Fody.Helpers;
using WorkingHours.DataBase;
using WorkingHours.DataBase.Models;
using WorkingHours.Models;
using WorkingHours.Utils;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        : this(Enumerable.Empty<WorkingTask>())
        {
        }

        public MainWindowViewModel(bool useDB)
        {
            if (useDB)
            {
                DBContext = new WorkingContext();

                Content = List = new WorkingTasksViewModel(
                    DBContext
                        .WorkingTasks
                        .Where(t => t.WorkingDay != null && t.WorkingDay.Date == DateTime.Today)
                        .Select(t => t.ToWorkingTask())
                );
            }
            else
            {
                Content = List = new WorkingTasksViewModel(Enumerable.Empty<WorkingTask>());
            }
        }

        public void Save()
        {
            DBContext?.Attach(new WorkingDayDBModel(new WorkingDay(List.Items.Select(t => t.Task), DateTime.Today)));
            DBContext?.SaveChanges();
        }

        private WorkingContext? DBContext { get; }

        public MainWindowViewModel(IEnumerable<WorkingTask> tasks)
        {
            Content = List = new WorkingTasksViewModel(tasks);
        }

        [Reactive] public ViewModelBase Content { get; set; }

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

            Observable.Merge(vm.Add, vm.Cancel.Select<Unit, WorkingTask?>(_ => null))
                .Take(1)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        var itemVm = new WorkingTaskItemViewModel(item);
                        List.Items.Add(itemVm);
                        new MiniMainWindow(itemVm)
                        {
                            Owner = WindowingUtils.GetMainWindow() as Window // hope it will be fixed some day https://github.com/AvaloniaUI/Avalonia/issues/3254
                        }.Show();
                    }
                    Content = List;
                });
            Content = vm;
        }

        public static void Pin()
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
