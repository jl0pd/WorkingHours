using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WorkingHours.Models;
using WorkingHours.Utils;

namespace WorkingHours.DataBase.Models
{
    public class WorkingDayDBModel
    {
        public WorkingDayDBModel()
        {
        }

        public WorkingDayDBModel(WorkingDay day)
        {
            day = day.ThrowIfNull(nameof(day));
            Date = day.Date;
            Tasks = day.Tasks.Select(t => new WorkingTaskDBModel(t)).ToList();
        }

        [Key] public int WorkingDayId { get; set; }

        public DateTime Date { get; set; }

        public ICollection<WorkingTaskDBModel>? Tasks { get; set; }

        public WorkingDay ToWorkingDay() 
            => new WorkingDay(
                Tasks?.Select(t => t.ToWorkingTask()) ?? Enumerable.Empty<WorkingTask>(), 
                Date);
    }
}
