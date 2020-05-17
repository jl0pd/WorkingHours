using Microsoft.EntityFrameworkCore;
using WorkingHours.Models;

namespace WorkingHours.DataBase
{
    class WorkingContext : DbContext
    {
        public DbSet<WorkingDay>? WorkingDays  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data source=WorkingTasks.db");
        }
    }
}
