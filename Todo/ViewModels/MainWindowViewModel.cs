using System.Reactive.Linq;
using System;
using ReactiveUI;
using Todo.Models;
using Todo.Services;
using DynamicData;
using System.Linq;
using System.Reactive;

namespace Todo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(Database db)
        {
            Content = List = new TodoListViewModel(db.GetItems());
        }

        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(vm.Ok, vm.Cancel.Select(_ => (TodoItem?)null))
                .Take(1)
                .Subscribe(item =>
                {
                    if (item != null)
                    {
                        List.Items.Add(item);
                    }
                    Content = List;
                });
            Content = vm;
        }

        private ViewModelBase _content = null!;

        public TodoListViewModel List { get; }
    }
}
