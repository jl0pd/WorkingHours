using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using ReactiveUI;
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
                    foreach (var item in e.NewItems.Cast<WorkingTaskItemViewModel>())
                    {
                        item.PropertyChanged += OnPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (var item in e.OldItems.Cast<WorkingTaskItemViewModel>())
                    {
                        item.PropertyChanged -= OnPropertyChanged;
                    }
                    break;
                }
            };

            Items.AddRange(items.Select(item => new WorkingTaskItemViewModel(item)));


            DeleteSelected = ReactiveCommand.Create(() =>
            {
                Items.RemoveMany(SelectedItems);
                SelectedItems.Clear();
            });
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OnPropertyChanged) && sender is WorkingTaskItemViewModel item)
            {
            }
        }

        public ReactiveCommand<Unit, Unit> DeleteSelected { get; }

        private List<WorkingTaskItemViewModel> SelectedItems { get; set; } = new List<WorkingTaskItemViewModel>();

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            // TODO: Make it work
            SelectedItems.Add(e.AddedItems.Cast<WorkingTaskItemViewModel>());
            SelectedItems.RemoveMany(e.RemovedItems.Cast<WorkingTaskItemViewModel>());
        }
    }
}
