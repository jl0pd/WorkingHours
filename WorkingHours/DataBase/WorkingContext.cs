using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WorkingHours.DataBase.Models;
using WorkingHours.Logging;

namespace WorkingHours.DataBase
{
    class WorkingContext : DbContext
    {
        public WorkingContext() : base()
        {
            var watch = Stopwatch.StartNew();
            Database.EnsureCreated();
            watch.Stop();
            Log.Debug(nameof(Database.EnsureCreated) + " {Elapsed}s", watch.Elapsed.TotalMilliseconds);
        }

        public DbSet<WorkingDayDBModel>? WorkingDays { get; set; }

        public DbSet<WorkingTaskDBModel>? WorkingTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=WorkingHours.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
