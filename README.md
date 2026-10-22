# CampusConnect — College Event Management System

A student mini-project built with ASP.NET Core MVC, Entity Framework Core, MySQL, Bootstrap and session state.

## Features
- Create, read, update and delete campus events (CRUD)
- Search events by title, category or venue
- Student event registration with validation
- Prevent duplicate registration for the same event using the same email
- Event capacity check
- Registration list and dashboard counts
- Responsive Bootstrap interface
- Session state remembers the most recently registered event title

## Requirements
- .NET 8 SDK
- MySQL Server (or XAMPP MySQL)
- Visual Studio 2022 or VS Code
- Internet for Bootstrap/jQuery CDN assets (or download assets locally for offline demo)

## Setup (Windows)
1. Install the .NET 8 SDK and MySQL Server. Start the MySQL service.
2. Open `appsettings.json`.
3. Change `YOUR_MYSQL_PASSWORD` to your MySQL root password. If your local root account has no password, leave `password=;`.
4. Open a terminal in this folder.
5. Run:
   ```powershell
   dotnet restore
   dotnet run
   ```
6. Open the HTTPS/HTTP URL printed in the terminal (usually `https://localhost:7xxx` or `http://localhost:5xxx`).
7. Use **+ Add Event** to create events, then test student registration.

The application uses `EnsureCreated()` to create the database and tables on first run. The MySQL database user must have permission to create a database. If a schema changes later, back up your data and recreate the development database or switch to EF Core migrations.

## MySQL connection example
`server=localhost;port=3306;database=college_events_db;user=root;password=YOUR_MYSQL_PASSWORD;`

## Project structure
- `Controllers/` — request handling and CRUD logic
- `Models/` — Event and EventRegistration entities with validation
- `Data/` — EF Core database context
- `Views/` — Razor pages
- `wwwroot/css/site.css` — responsive styling

## Important academic note
This is a teaching/demo starter, not a production-ready system. Event management routes are not protected by administrator authentication yet. For a production deployment, add ASP.NET Core Identity/role-based authorization, secrets management, logging, stronger privacy controls and migrations. Do not publish database passwords to GitHub.
