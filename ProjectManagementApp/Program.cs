using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Services;
using ProjectManagementDatabase;
using ProjectManagementEntities.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ProjectManagementContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ProjectManagementDatabase"))
    );

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<WorkPackageService>();
builder.Services.AddScoped<TaskItemService>();
builder.Services.AddScoped<ActivityService>();
builder.Services.AddScoped<TaskAssignmentService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<TeamService>();

builder.Services.AddScoped<ProjectTeamService>();
builder.Services.AddScoped<TimesheetEntryService>();
builder.Services.AddScoped<TeamMemberService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProjectManagementContext>();
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
