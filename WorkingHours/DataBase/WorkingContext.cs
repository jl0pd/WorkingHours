using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WorkingHours.DataBase.Models;
using WorkingHours.Logging;

namespace WorkingHours.DataBase
{
    internal class WorkingContext : DbContext
    {
        public WorkingContext(bool ensureCreated) : base()
        {
            if (ensureCreated)
            {
                var watch = Stopwatch.StartNew();
                Database.EnsureCreated();
                watch.Stop();
                Log.Debug(nameof(Database.EnsureCreated) + " {Elapsed}s", watch.Elapsed.TotalMilliseconds);
            }
        }

        public WorkingContext() : this(false)
        {
        }

        public DbSet<WorkingDayDBModel>? WorkingDays { get; set; }

        public DbSet<WorkingTaskDBModel>? WorkingTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingTaskDBModel>().HasKey(t => new { t.Created, t.WorkingTaskId });
            modelBuilder.Entity<WorkingDayDBModel>().HasKey(d => new { d.Date, d.WorkingDayId });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=WorkingHours.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
