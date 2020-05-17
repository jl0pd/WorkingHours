using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using Todo.Models;

namespace Todo.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        private int _checkedCount;

        public ObservableCollection<TodoItem> Items { get; }


        public TodoListViewModel(IEnumerable<TodoItem> items)
        {
            Items = new ObservableCollection<TodoItem>();


            Items.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                case NotifyCollectionChangedAction.Add:
                    foreach (TodoItem item in e.NewItems.Cast<TodoItem>())
                    {
                        item.PropertyChanged += OnPropertyChanged;
                        if (item.IsChecked)
                        {
                            CheckedCount++;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (TodoItem item in e.OldItems.Cast<TodoItem>())
                    {
                        item.PropertyChanged -= OnPropertyChanged;
                        CheckedCount--;
                    }
                    break;
                }
            };

            Items.AddRange(items);


            Remove = ReactiveCommand.Create(
                () => DeleteSelected(),
                this.WhenAnyValue(x => x.CheckedCount, x => x > 0));
        }

        private int CheckedCount
        {
            get => _checkedCount;
            set => this.RaiseAndSetIfChanged(ref _checkedCount, value);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsChecked) && sender is TodoItem item)
            {
                if (item.IsChecked)
                {
                    CheckedCount++;
                }
                else
                {
                    CheckedCount--;
                }
            }
        }

        public ReactiveCommand<Unit, Unit> Remove { get; }

        public void DeleteSelected() => Items.RemoveMany(Items.Where(i => i.IsChecked));
    }
}
