using System;
using System.Reactive;
using System.Timers;
using ReactiveUI;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingTaskItemViewModel : ViewModelBase
    {
        public WorkingTask Task { get; }

        private TimeSpan Elapsed => Task.Elapsed;

        private string WorkTimeString => Task.CurrentState switch
        {
            WorkingTask.State.NotStarted => "? - ?",
            WorkingTask.State.Completed => $"{Task.StartTime:HH:mm} - {Task.EndTime:HH:mm}",
            _ => $"{Task.StartTime:HH:mm} - ?",
        };

        private const int Second = 1_000;

        private Timer Timer { get; } = new Timer(60 * Second);

        public WorkingTaskItemViewModel(WorkingTask task)
        {
            Task = task;
            OnStartClick = ReactiveCommand.Create(() =>
            {
                Task.Start();
                Timer.Start();
                this.RaisePropertyChanged(nameof(WorkTimeString));
            });

            OnPauseClick = ReactiveCommand.Create(() =>
            {
                if (Task.CurrentState == WorkingTask.State.Paused)
                {
                    Task.Unpause();
                    Timer.Start();
                }
                else
                {
                    Task.Pause();
                    Timer.Stop();
                }
            });

            OnStopClick = ReactiveCommand.Create(() =>
            {
                Task.Stop();
                Timer.Stop();
                this.RaisePropertyChanged(nameof(WorkTimeString));
            });

            Timer.Elapsed += (sender, e) => this.RaisePropertyChanged(nameof(Elapsed));
        }

        public ReactiveCommand<Unit, Unit> OnStartClick { get; }
        public ReactiveCommand<Unit, Unit> OnPauseClick { get; }
        public ReactiveCommand<Unit, Unit> OnStopClick { get; }
    }
}
