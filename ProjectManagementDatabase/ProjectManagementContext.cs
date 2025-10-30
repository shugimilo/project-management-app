using Microsoft.EntityFrameworkCore;
using ProjectManagementEntities.Models;
using ProjectManagementDatabase;

namespace ProjectManagementDatabase
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options)
            : base(options)
        {
        }

        // DbSets (tables in your database)
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkPackage> WorkPackages { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<TimesheetEntry> TimesheetEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project -> WorkPackages (one-to-many)
            modelBuilder.Entity<Project>()
                .HasMany(p => p.WorkPackages)
                .WithOne(wp => wp.Project)
                .HasForeignKey(wp => wp.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // WorkPackage -> Tasks (one-to-many)
            modelBuilder.Entity<WorkPackage>()
                .HasMany(wp => wp.Tasks)
                .WithOne(t => t.WorkPackage)
                .HasForeignKey(t => t.WorkPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.WorkPackages)
                .WithOne(wp => wp.Team)
                .HasForeignKey(wp => wp.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // TaskItem -> Activities (one-to-many)
            modelBuilder.Entity<TaskItem>()
                .HasMany(t => t.Activities)
                .WithOne(a => a.TaskItem)
                .HasForeignKey(a => a.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // TeamMembers composite key
            modelBuilder.Entity<TeamMember>()
                .HasKey(tm => tm.EmployeeId);

            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Employee)
                .WithOne(e => e.TeamMembership)
                .HasForeignKey<TeamMember>(tm => tm.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // TaskAssignments composite key
            modelBuilder.Entity<TaskAssignment>()
                .HasKey(ta => new { ta.TaskItemId, ta.EmployeeId });

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.TaskItem)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(ta => ta.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.Employee)
                .WithMany(e => e.TaskAssignments)
                .HasForeignKey(ta => ta.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTeam>()
                .HasKey(pt => new { pt.ProjectId, pt.TeamId });

            modelBuilder.Entity<ProjectTeam>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTeams)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTeam>()
                .HasOne(pt => pt.Team)
                .WithMany(t => t.ProjectTeams)
                .HasForeignKey(pt => pt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TimesheetEntry>()
                .HasKey(te => te.Id);

            modelBuilder.Entity<TimesheetEntry>()
                .HasOne(te => te.Employee)
                .WithMany(e => e.TimesheetEntries)
                .HasForeignKey(te => te.EmployeeId);

            modelBuilder.Entity<TimesheetEntry>()
                .HasOne(te => te.Activity)
                .WithMany(a => a.TimesheetEntries)
                .HasForeignKey(te => te.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
