using Microsoft.EntityFrameworkCore;
using System;

namespace WebApp.Models
{
    public class HubContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(Environment.GetEnvironmentVariable("BUHCS"));

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Major> Majors{ get; set; }
        public DbSet<Offering> Offerings { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<TimeSlot> Timeslots { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}