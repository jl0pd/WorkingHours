using System.Reactive;
using ReactiveUI;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    class AddTaskViewModel : ViewModelBase
    {
        private string? _taskName;

        public AddTaskViewModel()
        {
            Add = ReactiveCommand.Create(
                () => new WorkingTask(TaskName!.Trim()), 
                this.WhenAnyValue(x => x.TaskName, x => !string.IsNullOrWhiteSpace(x)));
            
            Cancel = ReactiveCommand.Create(() => { });
        }

        public string? TaskName
        {
            get => _taskName;
            private set => this.RaiseAndSetIfChanged(ref _taskName, value);
        }

        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public ReactiveCommand<Unit, WorkingTask> Add { get; }
    }
}
