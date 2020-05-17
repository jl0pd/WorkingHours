using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reactive;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Serilog;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingTaskItemViewModel : ViewModelBase
    {
        private WorkingTask Task { get; }

        private TimeSpan Elapsed => Task.Elapsed;
        private DateTime StartTime => Task.StartTime;

        private Timer Timer { get; }

        public WorkingTaskItemViewModel(WorkingTask task)
        {
            Task = task;
            OnStartClick = ReactiveCommand.Create(() =>
            {
                Task.Start();
                Timer.Start();
                this.RaisePropertyChanged(nameof(StartTime));
                Debug.Write(StartTime);
            });

            OnPauseClick = ReactiveCommand.Create(() =>
            {
                Task.Pause();
                Timer.Stop();
            });

            OnStopClick = ReactiveCommand.Create(() =>
            {
                Task.Stop();
                Timer.Stop();
            });

            Timer = new Timer(1000);
            Timer.Elapsed += (sender, e) => this.RaisePropertyChanged(nameof(Elapsed));
        }

        public ReactiveCommand<Unit, Unit> OnStartClick { get; }
        public ReactiveCommand<Unit, Unit> OnPauseClick { get; }
        public ReactiveCommand<Unit, Unit> OnStopClick { get; }
    }
}
