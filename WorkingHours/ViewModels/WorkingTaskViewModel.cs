using System;
using System.Reactive;
using System.Timers;
using ReactiveUI;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingTaskViewModel : ViewModelBase<WorkingTask>
    {
        private string? Name => Model?.Name;
        private TimeSpan Elapsed => Model?.Elapsed ?? TimeSpan.Zero;

        private IObservable<WorkingTask.State> State
            => Model.WhenAnyValue(m => m.CurrentState);

        private string WorkTimeString => Model.CurrentState switch
        {
            WorkingTask.State.NotStarted => "? - ?",
            WorkingTask.State.Completed => $"{Model.StartTime:HH:mm} - {Model.EndTime:HH:mm}",
            _ => $"{Model.StartTime:HH:mm} - ?",
        };

        private const int Second = 1_000;

        private Timer Timer { get; } = new Timer(Second);

        public WorkingTaskViewModel() : this(new WorkingTask("Test")) { }

        public WorkingTaskViewModel(WorkingTask task)
        : base(task)
        {
            Model
                .WhenAny(m => m.CurrentState, s => s)
                .Subscribe(s =>
                {
                    switch (s.GetValue())
                    {
                        case WorkingTask.State.Started:
                            Timer.Start();
                            break;
                        default:
                            Timer.Stop();
                            break;
                    }
                    this.RaisePropertyChanged(nameof(WorkTimeString));
                });

            Timer.Elapsed += (sender, e) => this.RaisePropertyChanged(nameof(Elapsed));

            OnStartClick = ReactiveCommand.Create(() =>
            {
                Model?.Start();
                return this;
            });

            OnStopClick = ReactiveCommand.Create(() =>
            {
                Model?.Stop();
                return this;
            });

            OnCancelClick = ReactiveCommand.Create(() =>
            {
                Model?.Cancel();
                return this;
            });
        }

        public ReactiveCommand<Unit, WorkingTaskViewModel> OnCancelClick { get; }
        public ReactiveCommand<Unit, WorkingTaskViewModel> OnStartClick { get; }
        public ReactiveCommand<Unit, WorkingTaskViewModel> OnStopClick { get; }
    }
}
