using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
using WorkingHours.Models;

namespace WorkingHours.ViewModels
{
    public class WorkingDayViewModel : ViewModelBase<WorkingDay>
    {
        public WorkingDayViewModel()
        {
            if (Design.IsDesignMode)
            {
                Model = new WorkingDay(new List<WorkingTask>
                {
                    new WorkingTask(
                        "asd",
                        new DateTime(2020, 06, 05, 12, 14, 16),
                        new DateTime(2020, 06, 05, 12, 24, 16),
                        new DateTime(2020, 12, 3, 1, 2, 3),
                        WorkingTask.State.Completed)
                }, DateTime.Now);
            }
        }

        public WorkingDayViewModel(WorkingDay day)
        {
            Model = day;
        }

        public DateTime Date
            => Model?.Date ?? DateTime.MinValue;

        public IList<WorkingTask>? Tasks
            => Model?.Tasks;

        public IEnumerable<WorkingTaskViewModel>? TasksViewModel
            => Model?.Tasks.Select(t => new WorkingTaskViewModel(t));
    }
}
