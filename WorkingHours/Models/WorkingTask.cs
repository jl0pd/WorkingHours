using System;
using ReactiveUI;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public enum State { NotStarted, Started, Completed, Canceled }

        public State CurrentState { get; private set; } = State.NotStarted;

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
