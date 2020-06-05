using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    class AddTaskViewModel : ViewModelBase
    {
        public AddTaskViewModel()
        {
            Add = ReactiveCommand.Create(
                () => new WorkingTask(TaskName!.Trim()), 
                this.WhenAnyValue(x => x.TaskName, x => !string.IsNullOrWhiteSpace(x)));
            
            Cancel = ReactiveCommand.Create(() => { });
        }

        [Reactive] public string? TaskName { get; set; }

        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public ReactiveCommand<Unit, WorkingTask> Add { get; }
    }
}
