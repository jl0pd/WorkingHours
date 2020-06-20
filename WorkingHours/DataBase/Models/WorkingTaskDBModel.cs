using System;
using System.ComponentModel.DataAnnotations;
using WorkingHours.Models;
using WorkingHours.Utils;

namespace WorkingHours.DataBase.Models
{
    public class WorkingTaskDBModel
    {

        public WorkingTaskDBModel()
        {
        }

        internal WorkingTaskDBModel(WorkingTask task)
        {
            CurrentState = task.CurrentState;
            Name = task.Name;
            StartTime = task.StartTime;
            EndTime = task.EndTime;
            Created = task.Created;
        }

        [Key] public int WorkingTaskId { get; set; }

        [Required] public DateTime Created { get; set; }

        [Required] public WorkingTask.State CurrentState { get; set; }

        [Required] public string? Name { get; set; }

        [Required] public WorkingDayDBModel? WorkingDay { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        internal WorkingTask ToWorkingTask()
        {
            string name = Name.ThrowIfNull(nameof(Name));
            return new WorkingTask(name, StartTime, EndTime, Created, CurrentState);
        }
    }
}
