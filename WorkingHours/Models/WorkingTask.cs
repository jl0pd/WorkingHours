using System;
using ReactiveUI;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public enum State { NotStarted, Started, Paused, Completed, Canceled }

        public State CurrentState { get; private set; } = State.NotStarted;

        public WorkingTask(string name) => Name = name;

        public string Name { get; }

        public TimeSpan Elapsed => CurrentState switch
        {
            State.NotStarted => TimeSpan.Zero,
            State.Completed => EndTime - StartTime - TotalPausedTime,
            State.Started => DateTime.Now - StartTime,
            State.Paused => throw new NotImplementedException(),
            _ => throw new NotImplementedException()
        };

        public TimeSpan TotalPausedTime { get; private set; }

        private DateTime StartPauseTime { get; set; }

        private DateTime EndPauseTime { get; set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public void Start()
        {
            CurrentState = State.Started;
            StartTime = DateTime.Now;
        }

        public void Stop()
        {
            CurrentState = State.Completed;
            EndTime = DateTime.Now;
        }

        public void Pause()
        {
            CurrentState = State.Paused;
            StartPauseTime = DateTime.Now;
        }

        public void Unpause()
        {
            CurrentState = State.Started;
            EndPauseTime = DateTime.Now;
            TotalPausedTime += EndPauseTime - StartPauseTime;
        }

        public void Cancel() => CurrentState = State.Canceled;
    }
}
