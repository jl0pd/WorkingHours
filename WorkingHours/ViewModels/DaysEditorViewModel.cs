using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WorkingHours.Logging;
using WorkingHours.Models;
using WorkingHours.Utils;
using WorkingHours.Views;

namespace WorkingHours.ViewModels
{
    public class DaysEditorViewModel : ViewModelBase<ObservableCollection<WorkingDayViewModel>>
    {
        public ObservableCollection<WorkingDayViewModel>? DaysViewModels => Model;

        public IEnumerable<WorkingDay>? Days
            => Model?.Where(m => !(m.Model is null)).Select(d => d.Model!);

        [Reactive] public WorkingDayViewModel? SelectedDay { get; set; }

        private int TasksCount => SelectedDay?.Tasks?.Count ?? 0;

        public DaysEditorViewModel(IEnumerable<WorkingDay>? days)
        : base(new ObservableCollection<WorkingDayViewModel>(days?.Select(d => new WorkingDayViewModel(d))))
        {
            var currentDay = Model?.FirstOrDefault(d => d.Date == DateTime.Today);
            currentDay ??= Model?.FirstOrDefault();
            if (!(currentDay is null))
            {
                SelectedDay = currentDay;
            }

            Add = ReactiveCommand.Create(() =>
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
                            SelectedDay?.Tasks?.Add(item);
                            new MiniMainWindow(itemVm)
                            {
                                Owner = WindowingUtils.GetMainWindow() as Window // hope it will be fixed some day https://github.com/AvaloniaUI/Avalonia/issues/3254
                            }.Show();
                        }
                    });
                return vm;
            });

            ShowElapsed = ReactiveCommand.Create(() =>
            {
                var vm = new TotalElapsedViewModel(SelectedDay?.Tasks ?? Enumerable.Empty<WorkingTask>());
                return vm;
            });
        }


        public DaysEditorViewModel()
        : this(null)
        {
        }

        public ReactiveCommand<Unit, AddTaskViewModel> Add { get; }

        public ReactiveCommand<Unit, TotalElapsedViewModel> ShowElapsed { get; }
    }
}
