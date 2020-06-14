using Microsoft.EntityFrameworkCore;
using WorkingHours.DataBase.Models;

namespace WorkingHours.DataBase
{
    class WorkingContext : DbContext
    {

        public WorkingContext()
        : base()
        {
            Database.EnsureCreated();
        }

        public DbSet<WorkingDayDBModel>? WorkingDays  { get; set; }

        public DbSet<WorkingTaskDBModel>? WorkingTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=WorkingHours.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
