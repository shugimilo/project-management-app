using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;

namespace ProjectManagementDatabase
{
    public class ProjectManagementContextFactory : IDesignTimeDbContextFactory<ProjectManagementContext>
    {
        public ProjectManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementContext>();
            optionsBuilder.UseSqlite("Data Source=ProjectManagement.db"); // your DB path
            return new ProjectManagementContext(optionsBuilder.Options);
        }
    }
}
