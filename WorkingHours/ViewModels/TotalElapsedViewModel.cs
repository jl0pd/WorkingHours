using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    class TotalElapsedViewModel : ViewModelBase
    {
        public TotalElapsedViewModel(IEnumerable<WorkingTask> enumerable)
        {
            Tasks = Array.AsReadOnly(enumerable.GroupBy(t => t.Name).Select(g => new WorkingTaskGroup(g, g.Key)).ToArray());
        }

        public TimeSpan TotalWorkTime => Tasks.Any()
            ? Tasks.Select(t => t.Elapsed).Aggregate((acc, t) => acc + t)
            : TimeSpan.Zero;

        private IReadOnlyList<WorkingTaskGroup> Tasks { get; }

        public ReactiveCommand<Unit, Unit> Back { get; } = ReactiveCommand.Create(() => { });

        private class WorkingTaskGroup
        {
            public WorkingTaskGroup(IEnumerable<WorkingTask> tasks)
            {
                IEnumerator<WorkingTask> iter = tasks.GetEnumerator();
                while (iter.MoveNext())
                {
                    Elapsed += iter.Current.Elapsed;
                    Name = iter.Current.Name;
                }
            }

            public WorkingTaskGroup(IEnumerable<WorkingTask> tasks, string name)
            {
                Elapsed = tasks.Select(t => t.Elapsed).Aggregate((acc, s) => acc + s);
                Name = name;
            }

            public string Name { get; } = "";
            public TimeSpan Elapsed { get; }
        }
    }
}
