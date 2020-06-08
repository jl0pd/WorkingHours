using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using WorkingHours.Models;
using WorkingHours.Providers;
using WorkingHours.Utils;

namespace WorkingHours.ViewModels
{
    public class WorkingTasksViewModel : ViewModelBase
    {
        public ObservableCollection<WorkingTaskItemViewModel> Items { get; }

        public WorkingTasksViewModel(IEnumerable<WorkingTask> items)
        : this (items.Select(t => new WorkingTaskItemViewModel(t)))
        {
        }

        public WorkingTasksViewModel(IEnumerable<WorkingTaskItemViewModel> viewModels)
        {
            Items = new ObservableCollection<WorkingTaskItemViewModel>(viewModels);
            Items.ToObservableChangeSet().OnItemAdded(vm =>
            {
                vm.OnCancelClick.Subscribe(async item =>
                {
                    var res = await DialogProvider.ShowDialog($"Remove task '{item.Task.Name}'?", "Remove", 
                        IDialogService.ButtonType.YesNo, WindowingUtils.GetMainWindow()).ConfigureAwait(true);

                    if (res == IDialogService.DialogResult.Yes)
                    {
                        Items.Remove(item);
                    }
                });
            }).Subscribe();
        }
    }
}
