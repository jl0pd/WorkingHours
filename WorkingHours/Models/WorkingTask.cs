using System;
using ReactiveUI;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public enum State { NotStarted, Started, Paused, Completed }

        public State CurrentState { get; private set; } = State.NotStarted;

        public WorkingTask(string name) => Name = name;

        public string Name { get; }

        public bool IsPaused => CurrentState == State.Paused;

        public bool IsCompleted => CurrentState == State.Completed;

        public TimeSpan Elapsed => CurrentState switch
        {
            State.NotStarted => TimeSpan.Zero,
            State.Completed => EndTime - StartTime - TotalPausedTime,
            State.Started  => DateTime.Now - StartTime,
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

        public void Stop() => EndTime = DateTime.Now;

        public void Pause() { }

        public void Unpause() { }

        public void Cancel() { }
    }
}
