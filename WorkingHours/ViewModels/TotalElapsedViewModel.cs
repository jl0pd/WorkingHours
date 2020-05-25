using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            Tasks = enumerable.GroupBy(t => t.Name).Select(g => new WorkingTaskGroup(g, g.Key)).ToImmutableArray();

            Back = ReactiveCommand.Create(() => { });
        }

        public TimeSpan TotalWorkTime => Tasks.Select(t => t.Elapsed).Aggregate((acc, t) => acc + t);

        private IReadOnlyList<WorkingTaskGroup> Tasks { get; }

        public ReactiveCommand<Unit, Unit> Back { get; }

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
