using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingTasksViewModel : ViewModelBase
    {
        public ObservableCollection<WorkingTaskItemViewModel> Items { get; }

        public WorkingTasksViewModel(IEnumerable<WorkingTask> items)
        {
            Items = new ObservableCollection<WorkingTaskItemViewModel>();


            Items.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                case NotifyCollectionChangedAction.Add:
                    foreach (WorkingTaskItemViewModel? item in e.NewItems)
                    {
                        item?.OnCancelClick.Take(1).Subscribe(t => Items.Remove(t));
                    }
                    break;
                }
            };

            Items.AddRange(items.Select(item => new WorkingTaskItemViewModel(item)));
        }
    }
}
