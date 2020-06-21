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
using WorkingHours.Logging;
using WorkingHours.Models;
using WorkingHours.Utils;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        : this(false)
        {
        }

        public MainWindowViewModel(bool useDB)
        {
            ICollection<WorkingDay>? days = null;
            if (UseDB = useDB)
            {
                using var dbContext = new WorkingContext(true);
                days = dbContext
                        .WorkingDays
                        .Where(d => d.Date.AddDays(7) > DateTime.Today)
                        .Select(d => d.ToWorkingDay())
                        .ToList();
                Log.Info("Loaded {Days}", days);
            }

            Content = List = new DaysEditorViewModel(
                days is null || days.Count == 0
                    ? Enumerable.Repeat(new WorkingDay(), 1)
                    : days);
        }

        public bool UseDB { get; }

        public void Save()
        {
            if (UseDB)
            {
                using var dbContext = new WorkingContext();

                IEnumerable<WorkingDay>? days = List.Model.Where(d => !(d.Model is null)).Select(d => d.Model!);

                if (!(days is null))
                {
                    dbContext.UpdateRange(days.Select(d => new WorkingDayDBModel(d)));
                    try
                    {
                        //dbContext.SaveChanges();
                    }
                    catch(Microsoft.EntityFrameworkCore.DbUpdateException e)
                    {
                        Log.Error("{Error}", e);
                    }
                }
            }
        }

        [Reactive] public ViewModelBase Content { get; set; }

        public void ShowElapsed()
        {
            var vm = new TotalElapsedViewModel(List.SelectedDay?.Tasks ?? Enumerable.Empty<WorkingTask>());
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

            Observable
                .Merge(vm.Add, vm.Cancel.Select<Unit, WorkingTask?>(_ => null))
                .Take(1)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        Log.Info("Created item {@Item}", item);
                        var itemVm = new WorkingTaskViewModel(item);
                        List.SelectedDay?.Tasks?.Add(item);
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

        public DaysEditorViewModel List { get; }
    }
}
