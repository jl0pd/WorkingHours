using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    internal class AddTaskViewModel : ViewModelBase
    {
        public AddTaskViewModel()
        {
            var canAdd = this.WhenAnyValue(x => x.TaskName, x => !string.IsNullOrWhiteSpace(x));

            Add = ReactiveCommand.Create(
                () => new WorkingTask(TaskName!.Trim()), 
                canAdd);
        }

        [Reactive] public string? TaskName { get; set; }

        public ReactiveCommand<Unit, Unit> Cancel { get; } = ReactiveCommand.Create(() => { });

        public ReactiveCommand<Unit, WorkingTask> Add { get; }
    }
}
