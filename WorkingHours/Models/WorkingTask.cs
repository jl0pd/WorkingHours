using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public enum State { None, NotStarted, Started, Completed, Canceled }

        [Reactive] public State CurrentState { get; private set; } = State.NotStarted;

        public WorkingTask(string name)
        : this(name, DateTime.Now)
        {
        }

        public WorkingTask(string name, DateTime created) => (Name, Created) = (name, created);

        public WorkingTask(string name, DateTime? startTime, DateTime? endTime, DateTime created, State state)
        : this(name, created)
        {
            StartTime = startTime;
            EndTime = endTime;
            CurrentState = state;
        }

        public string Name { get; }

        public TimeSpan Elapsed => CurrentState switch
        {
            State.Started => DateTime.Now - StartTime!.Value,
            State.Completed => EndTime!.Value - StartTime!.Value,
            _ => TimeSpan.Zero,
        };

        public DateTime Created { get; }

        [Reactive] public DateTime? StartTime { get; private set; }

        [Reactive] public DateTime? EndTime { get; private set; }

        public void Start()
        {
            StartTime ??= DateTime.Now;
            CurrentState = State.Started;
        }

        public void Stop()
        {
            DateTime now = DateTime.Now;
            StartTime ??= now;
            EndTime ??= now;
            CurrentState = State.Completed;
        }

        public void Cancel()
        {
            DateTime now = DateTime.Now;
            StartTime ??= now;
            EndTime ??= now;
            CurrentState = State.Canceled;
        }
    }
}
