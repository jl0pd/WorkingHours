using System;
using ReactiveUI;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public enum State { NotStarted, Started, Completed, Canceled }

        public State CurrentState { get; private set; } = State.NotStarted;

        public WorkingTask(string name) => Name = name;

        public WorkingTask(string name, DateTime? startTime, DateTime? endTime, State state)
        : this(name)
        {
            StartTime = startTime;
            EndTime = endTime;
            CurrentState = state;
        }

        public WorkingDay Day { get; }

        public string Name { get; }

        public TimeSpan Elapsed => CurrentState switch
        {
            State.Started => DateTime.Now - StartTime!.Value,
            State.Completed => EndTime!.Value - StartTime!.Value,
            _ => TimeSpan.Zero,
        };

        public DateTime? StartTime { get; private set; }

        public DateTime? EndTime { get; private set; }

        public void Start()
        {
            CurrentState = State.Started;
            StartTime ??= DateTime.Now;
        }

        public void Stop()
        {
            CurrentState = State.Completed;
            StartTime ??= EndTime ??= DateTime.Now;
        }

        public void Cancel()
        {
            StartTime ??= EndTime ??= DateTime.Now;
            CurrentState = State.Canceled;
        }
    }
}
