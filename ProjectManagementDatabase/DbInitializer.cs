using System;
using System.Linq;
using ProjectManagementEntities.Models;

namespace ProjectManagementDatabase
{
    public static class DbInitializer
    {
        public static void Initialize(ProjectManagementContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // If there are already employees, assume DB is seeded
            if (context.Employees.Any())
                return;

            // 1️⃣ Employees
            var employees = new Employee[]
            {
                new Employee { FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com" },
                new Employee { FirstName = "Bob", LastName = "Smith", Email = "bob@example.com" },
                new Employee { FirstName = "Charlie", LastName = "Brown", Email = "charlie@example.com" }
            };
            context.Employees.AddRange(employees);
            context.SaveChanges();

            // 2️⃣ Teams
            var teams = new Team[]
            {
                new Team { Name = "Development Team" },
                new Team { Name = "QA Team" }
            };
            context.Teams.AddRange(teams);
            context.SaveChanges();

            // 3️⃣ TeamMembers
            var teamMembers = new TeamMember[]
            {
                new TeamMember { Team = teams[0], Employee = employees[0], RoleInTeam = "Developer" },
                new TeamMember { Team = teams[0], Employee = employees[1], RoleInTeam = "Developer" },
                new TeamMember { Team = teams[1], Employee = employees[2], RoleInTeam = "QA Engineer" }
            };
            context.TeamMembers.AddRange(teamMembers);
            context.SaveChanges();

            // 4️⃣ Projects
            var projects = new Project[]
            {
                new Project
                {
                    Name = "Project Alpha",
                    Description = "First project",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(6))
                },
                new Project
                {
                    Name = "Project Beta",
                    Description = "Second project",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(3))
                }
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();

            // 5️⃣ WorkPackages (use navigation property instead of Id)
            var workPackages = new WorkPackage[]
            {
                new WorkPackage { Name = "Backend Development", Description = "Develop backend services", Project = projects[0] },
                new WorkPackage { Name = "Frontend Development", Description = "Develop frontend application", Project = projects[0] } // Both in Project Alpha
            };
            context.WorkPackages.AddRange(workPackages);
            context.SaveChanges();

            // 6️⃣ TaskItems (use navigation property instead of Id)
            var tasks = new TaskItem[]
            {
                new TaskItem { Name = "Design Database", Description = "Design the initial database schema", WorkPackage = workPackages[0] },
                new TaskItem { Name = "Implement API", Description = "Develop the RESTful API", WorkPackage = workPackages[0] },
                new TaskItem { Name = "Create UI Mockups", Description = "Design frontend mockups", WorkPackage = workPackages[1] }
            };
            context.TaskItems.AddRange(tasks);
            context.SaveChanges();
        }
    }
}
