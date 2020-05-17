using System;
using ReactiveUI;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public WorkingTask(string name) => Name = name;

        public string Name { get; }

        public bool IsPaused { get; private set; }

        public bool IsCompleted { get; private set; }

        public TimeSpan Elapsed => 
            StartTime == DateTime.MinValue 
                ? TimeSpan.Zero 
                : EndTime == DateTime.MinValue 
                    ? DateTime.Now - StartTime 
                    : StartTime - EndTime - PausedTime;

        public TimeSpan PausedTime { get; private set; }

        public DateTime StartTime { get; private set; }
        
        public DateTime EndTime { get; private set; }

        public void Start() => StartTime = DateTime.Now;

        public void Stop() => EndTime = DateTime.Now;

        public void Pause() { }

        public void Unpause() { }

        public void Cancel() { }
    }
}
