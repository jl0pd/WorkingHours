using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WorkingHours.Models
{
    class WorkingDay
    {
        public WorkingDay()
        : this(DateTime.Today)
        {
        }

        public WorkingDay(DateTime date)
        : this (Enumerable.Empty<WorkingTask>(), date)
        {
        }

        public WorkingDay(IEnumerable<WorkingTask> tasks, DateTime date)
        {
            Tasks = new ObservableCollection<WorkingTask>(tasks);
            Date = date.Date;
        }

        public ObservableCollection<WorkingTask> Tasks { get; }

        public DateTime Date { get; }
    }
}
