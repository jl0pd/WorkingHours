using System;
using System.Reactive;
using System.Timers;
using ReactiveUI;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingTaskViewModel : ViewModelBase<WorkingTask>
    {
        public WorkingTask Task { get; }

        private TimeSpan Elapsed => Task.Elapsed;

        private WorkingTask.State State => Task.CurrentState;

        private string WorkTimeString => Task.CurrentState switch
        {
            WorkingTask.State.NotStarted => "? - ?",
            WorkingTask.State.Completed => $"{Task.StartTime:HH:mm} - {Task.EndTime:HH:mm}",
            _ => $"{Task.StartTime:HH:mm} - ?",
        };

        private const int Second = 1_000;

        private Timer Timer { get; } = new Timer(Second);

        public WorkingTaskViewModel() : this(new WorkingTask("Test")) { }

        public WorkingTaskViewModel(WorkingTask task)
        {
            Task = task;

            Timer.Elapsed += (sender, e) => this.RaisePropertyChanged(nameof(Elapsed));

            OnStartClick = ReactiveCommand.Create(() =>
            {
                Task.Start();
                Timer.Start();
                this.RaisePropertyChanged(nameof(WorkTimeString));
                this.RaisePropertyChanged(nameof(State));
                return this;
            });

            OnStopClick = ReactiveCommand.Create(() =>
            {
                Task.Stop();
                Timer.Stop();
                this.RaisePropertyChanged(nameof(WorkTimeString));
                this.RaisePropertyChanged(nameof(State));
                return this;
            });

            OnCancelClick = ReactiveCommand.Create(() =>
            {
                this.RaisePropertyChanged(nameof(State));
                return this;
            });
        }

        public ReactiveCommand<Unit, WorkingTaskViewModel> OnCancelClick { get; }

        public ReactiveCommand<Unit, WorkingTaskViewModel> OnStartClick { get; }

        public ReactiveCommand<Unit, WorkingTaskViewModel> OnStopClick { get; }
    }
}
