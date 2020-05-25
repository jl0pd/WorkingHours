﻿using System;
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

        private Timer Timer { get; } = new Timer(Second);

        public WorkingTaskItemViewModel(WorkingTask task)
        {
            Task = task;

            Timer.Elapsed += (sender, e) => this.RaisePropertyChanged(nameof(Elapsed));

            OnStartClick = ReactiveCommand.Create(() =>
            {
                Task.Start();
                Timer.Start();
                this.RaisePropertyChanged(nameof(WorkTimeString));
            });

            OnStopClick = ReactiveCommand.Create(() =>
            {
                Task.Stop();
                Timer.Stop();
                this.RaisePropertyChanged(nameof(WorkTimeString));
            });

            OnCancelClick = ReactiveCommand.Create(() => this);
        }

        public ReactiveCommand<Unit, WorkingTaskItemViewModel> OnCancelClick { get; }

        public ReactiveCommand<Unit, Unit> OnStartClick { get; }

        public ReactiveCommand<Unit, Unit> OnStopClick { get; }
    }
}
