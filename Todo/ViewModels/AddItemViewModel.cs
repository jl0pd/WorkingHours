using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using Todo.Models;

namespace Todo.ViewModels
{
    class AddItemViewModel : ViewModelBase
    {
        private string? _description;

        public string? Description
        {
            get => _description; 
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public AddItemViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x)
            );

            Ok = ReactiveCommand.Create(
                () => new TodoItem(Description),
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }


        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
