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
        public ObservableCollection<WorkingTask> Items { get; }


        public WorkingTasksViewModel(IEnumerable<WorkingTask> items)
        {
            Items = new ObservableCollection<WorkingTask>
            {
                new WorkingTask("1243"),
                new WorkingTask("1245"),
                new WorkingTask("1246"),
            };

            Items.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                case NotifyCollectionChangedAction.Add:
                    foreach (WorkingTask item in e.NewItems.Cast<WorkingTask>())
                    {
                        item.PropertyChanged += OnPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (WorkingTask item in e.OldItems.Cast<WorkingTask>())
                    {
                        item.PropertyChanged -= OnPropertyChanged;
                    }
                    break;
                }
            };

            Timer = new Timer(1000);
            Timer.Elapsed += (sender, e) =>
            {
                foreach (WorkingTask t in items)
                {
                    t.RaisePropertyChanged(nameof(WorkingTask.Elapsed));
                }
            };
            Timer.Start();
            Items.AddRange(items);

            OnStartClick = ReactiveCommand.Create(execute: (Button b) =>
           {
               var a = (WorkingTask)b.DataContext;
               a.Start();
           });

            DeleteSelected = ReactiveCommand.Create(() =>
            {
                Items.RemoveMany(SelectedItems);
                SelectedItems.Clear();
            });
        }

        public ReactiveCommand<Button, Unit> OnStartClick { get; }
        public ReactiveCommand<Button, Unit> OnPauseClick { get; }
        public ReactiveCommand<Button, Unit> OnStopClick { get; }


        private Timer Timer { get; }

        private ListBox MainListBox { get; }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OnPropertyChanged) && sender is WorkingTask item)
            {
            }
        }

        public ReactiveCommand<Unit, Unit> DeleteSelected { get; }

        //private void DeleteSelected()
        //{
        //    Items.RemoveMany(SelectedItems);
        //    SelectedItems.Clear();
        //}

        private List<WorkingTask> SelectedItems { get; set; } = new List<WorkingTask>();

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            // TODO: Make it work
            SelectedItems.Add(e.AddedItems.Cast<WorkingTask>());
            SelectedItems.RemoveMany(e.RemovedItems.Cast<WorkingTask>());
        }
    }
}
