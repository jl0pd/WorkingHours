using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using ReactiveUI.Fody.Helpers;
using WorkingHours.DataBase;
using WorkingHours.DataBase.Models;
using WorkingHours.Logging;
using WorkingHours.Models;
using WorkingHours.Utils;

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
                        .Include(d => d.Tasks)
                        .Select(d => d.ToWorkingDay())
                        .ToList();
                Log.Info("Loaded {Days}", days);
            }

            Content = List = new DaysEditorViewModel(
                days is null || days.Count == 0
                    ? Enumerable.Repeat(new WorkingDay(), 1)
                    : days);

            List.Add.Subscribe(vm =>
            {
                Observable
                    .Merge(vm.Add, vm.Cancel.Select(_ => (WorkingTask?)null))
                    .Take(1)
                    .Subscribe(_ => Content = List);

                Content = vm;
            });

            List.ShowElapsed.Subscribe(vm =>
            {
                vm.Back.Subscribe(_ => Content = List);
                Content = vm;
            });
        }

        public bool UseDB { get; }

        public void Save()
        {
            if (UseDB)
            {
                using var dbContext = new WorkingContext();

                IList<WorkingDay>? days = List
                                            .Model
                                            .Where(d => !(d.Model is null))
                                            .Select(d => d.Model!)
                                            .ToArray();

                if (!(days is null))
                {
                    var inMemoryDays = days.Select(d => d.Date).ToList();
                    var dbDays = dbContext.WorkingDays.Select(d => d.Date).Where(d => inMemoryDays.Contains(d)).ToList();
                    var groups = days.GroupBy(d => dbDays.Contains(d.Date));

                    foreach (var group in groups)
                    {
                        if (group.Key) // already stored in DB
                        {
                            dbContext.UpdateRange(group.Select(d => new WorkingDayDBModel(d)));
                        }
                        else // new
                        {
                            dbContext.AddRange(group.Select(d => new WorkingDayDBModel(d)));
                        }
                    }

                    try
                    {
                        dbContext.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        Log.Error("{Error}", e);
                    }
                }
            }
        }

        [Reactive] public ViewModelBase Content { get; set; }

        public void ShowSettings()
        {
            var vm = new SettingsPanelViewModel();
            vm.Back.Take(1).Subscribe(_ => Content = List);
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
