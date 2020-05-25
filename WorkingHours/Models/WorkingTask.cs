﻿using System;
using ReactiveUI;

namespace WorkingHours.Models
{
    public class WorkingTask : ReactiveObject
    {
        public enum State { NotStarted, Started, Completed, Canceled }

        public State CurrentState { get; private set; } = State.NotStarted;

        public WorkingTask(string name) => Name = name;

        public string Name { get; }

        public TimeSpan Elapsed => CurrentState switch
        {
            State.NotStarted => TimeSpan.Zero,
            State.Completed => EndTime - StartTime,
            State.Started => DateTime.Now - StartTime,
            _ => throw new NotImplementedException()
        };

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

        public void Cancel()
        {
            EndTime = DateTime.Now;
            CurrentState = State.Canceled;
        }
    }
}
