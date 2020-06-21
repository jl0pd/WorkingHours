using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using WorkingHours.Models;
using WorkingHours.Providers;
using WorkingHours.Utils;
using static WorkingHours.Models.IDialogService;

namespace WorkingHours.ViewModels
{
    public class WorkingDayViewModel : ViewModelBase<WorkingDay>
    {
        public WorkingDayViewModel()
        {
#if DEBUG
            if (Design.IsDesignMode)
            {
                Model = new WorkingDay(new List<WorkingTask>
                {
                    new WorkingTask(
                        "asd",
                        new DateTime(2020, 06, 05, 12, 14, 16),
                        new DateTime(2020, 06, 05, 12, 24, 27),
                        new DateTime(2020, 12, 3, 1, 2, 3),
                        WorkingTask.State.Completed)
                }, DateTime.Now);
            }
#endif
        }

        public WorkingDayViewModel(WorkingDay day)
        : base(day)
        {
        }

        public override WorkingDay? Model
        {
            get => base.Model;
            set
            {
                value?.Tasks
                .ToObservableChangeSet()
                .ActOnEveryObject(t =>
                {
                    this.RaisePropertyChanged(nameof(Tasks));
                    this.RaisePropertyChanged(nameof(TasksViewModels));
                }, t =>
                {
                    this.RaisePropertyChanged(nameof(Tasks));
                    this.RaisePropertyChanged(nameof(TasksViewModels));
                });
                base.Model = value;
            }
        }

        public DateTime Date
            => Model?.Date ?? DateTime.MinValue;

        public IList<WorkingTask>? Tasks
            => Model?.Tasks;

        public IEnumerable<WorkingTaskViewModel>? TasksViewModels
            => Model?.Tasks.Select(t => CreateVM(t));

        private WorkingTaskViewModel CreateVM(WorkingTask task)
        {
            var vm = new WorkingTaskViewModel(task);
            vm.OnCancelClick.Subscribe(async vm =>
            {
                DialogResult res = await DialogProvider.ShowDialog($"Remove task '{vm.Model!.Name}'?", "Remove",
                                ButtonType.YesNo, WindowingUtils.GetMainWindow()).ConfigureAwait(true);

                if (res == DialogResult.Yes)
                {
                    Model!.Tasks.Remove(vm.Model!);
                }
            });
            return vm;
        }
    }
}
