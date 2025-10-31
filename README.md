# Project Management App

A web application for managing projects, work packages, tasks and activities.  
Implements a hierarchical work-unit model (Projects → Work Packages → Tasks → Activities) with teams and employees. The app demonstrates an enterprise-style design using Razor Pages, Entity Framework Core and SQLite for lightweight persistence, focusing on clear data modelling, progress calculation and navigable CRUD interfaces.

---

## Project Structure & Architecture

The solution is structured to clearly separate concerns using a layered approach:
**1. ProjectManagementApp (Main Web App)**
  - Contains Razor Pages for all entities, grouped in subdirectories (e.g., Pages/Activities, Pages/Employees). Each subdirectory has CRUD pages (Index, Details, Create, Edit, Delete).
  - Uses services for business logic (ActivityService, EmployeeService, etc.), which communicate with the database project.
**2. ProjectManagementDatabase**
  - Handles data access and EF Core logic.
  - Contains DbContext, DbContextFactory, migrations, and a DB initializer.
**3. ProjectManagementEntities**
  - Holds entity classes/models and enums for the database.
  - Isolated domain layer ensures a clean separation between data models and business logic.

**Approach:**
  - **Layered Architecture** with four main layers:
      - **Presentation Layer:** Razor Pages + Page Models
      - **Business Logic / Services Layer:** Entity-specific services
      - **Data Access Layer:** EF Core DbContext and database logic
      - **Domain Layer:** Entity classes and enums
  - **Composite pattern** is used for progress calculation: parent entities compute progress from children based on planned vs actual hours.

This architecture provides maintainability, clear separation of concerns, and testability, while remaining simple enough for educational/demo purposes.

---

## Key features

- **Hierarchical work model:** Projects contain Work Packages; Work Packages contain Tasks; Tasks contain Activities.  
- **Team & employee model:** Multiple teams can be assigned to a project; a single work package is assigned to one team. Tasks are assigned to employees within the same team.  
- **Progress calculation (Composite pattern):** Activities include `PlannedHours`. Actual hours are computed by summing timesheet entries for the given activity and employee; parent entities compute progress from their children.  
- **Full CRUD:** Create, read, update and delete operations for Projects, Work Packages, Tasks, Activities, Teams and Employees.  
- **Razor Pages UI:** Server-rendered frontend using Razor Pages (.cshtml + page models), Bootstrap, and minimal jQuery for small UI behaviors.  
- **Designed for integrity:** Timesheet entries cannot be created from the public UI (timesheets are computed/derived), preventing easy manipulation of actual hours by users.  
- **Lightweight persistence:** Uses SQLite for local/demo deployments (easy to inspect with SQLite Studio).

---

## Tech stack

- **Framework:** .NET 8 (ASP.NET Core Razor Pages)  
- **ORM:** Entity Framework Core (SQLite provider)  
- **Frontend:** Razor Pages (.cshtml), Bootstrap, minimal jQuery  
- **Database:** SQLite (file-based DB; recommended for development/demo)  
- **IDE:** Visual Studio 2022 Community Edition (recommended) or VS Code with .NET SDK  
- **Extras:** SQLite Studio for DB inspection

---

## Quick start — Visual Studio (recommended)

1. **Clone the repo**
```bash
git clone https://github.com/shugimilo/project-management-app.git
cd project-management-app
```
2. Open the solution
Open the .sln file in Visual Studio 2022.

3. Restore NuGet packages
Visual Studio usually restores automatically. If not: Build → Restore NuGet Packages.

4. Check connection string
Open appsettings.json and verify the SQLite connection string (e.g. Data Source=app.db).

5. Run the app
Press F5 (or click Run) to build and run. Visual Studio will launch the application in your browser.

6. Inspect database (optional)
Use [SQLite Studio](https://sqlitestudio.pl/) to open the SQLite file (e.g. app.db) and inspect tables, rows and relationships.

---

## Quick start — Command line (.NET CLI)

1. Prerequisites
.NET 8 SDK installed
(Optional) Visual Studio Code or another editor

2. Clone & navigate
```bash
git clone https://github.com/shugimilo/project-management-app.git
cd project-management-app/ProjectManagementApp
```

3. Restore & build
```bash
dotnet restore
dotnet build
```

4. (Optional) Apply EF migrations
If the project uses migrations and includes them:
```bash
dotnet ef database update
```

5. Run
```bash
dotnet run
```

6. Open the URL printed in the terminal (usually https://localhost:5001 or http://localhost:5000).

---

## Database notes

- SQLite file location is configured in appsettings.json.
- If you want to inspect or modify the DB directly, use SQLite Studio.
- Recommended ignores in .gitignore: bin/, obj/, *.db (or the specific DB filename) — so you don't accidentally commit the DB file.

---

## Application behaviour & UX notes

- UI is intentionally simple and navigable: each Index and Details page has links to related entities to ease traversal (e.g., from a Project to its Work Packages).
- The app does not include user accounts or notifications. It is intended for structure and overview rather than multi-user production usage.
- Timesheet entries are not created via public UI to prevent manual manipulation of actual hours. The ActualHours for activities are computed by summing related timesheet records in the DB.

---

## Design & architecture

- Layered solution structure: The solution includes separate projects for entities, data access (DbContext), and the web app, aligned with common educational templates.
- Entity model: The data model includes tables for Projects, WorkPackages, Tasks, Activities, Teams, Employees, and TimesheetEntries with appropriate relational constraints.
- Composite pattern: Parent entities compute progress from child elements (planned vs actual). Activity PlannedHours is stored; ActualHours is derived by summing timesheet entries for (activity, employee).

---

## Author

Petar Milojević — GitHub: [shugimilo](https://github.com/shugimilo)

If you have questions or want a walkthrough of the codebase, open an issue in this repository or contact me via GitHub.
