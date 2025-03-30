
# How to Add Migrations Using a Startup Project in EF Core

## 1. Prerequisites
Ensure the following:
- The `dotnet-ef` CLI tool is installed globally or locally:
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- Both the class library and the startup project reference the following packages:
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
  ```
- The `DbContext` is properly configured in the startup project.

## 2. Command to Add Migrations
Run the following command from the directory of the project containing your `DbContext`:

```bash
dotnet ef migrations add <MigrationName> -o <MigrationsDirectory> --startup-project <StartupProject>
```

### Example:
```bash
dotnet ef migrations add InitialCreate -o Persistence/Migrations --startup-project ../Api
```

### Parameters:
- `<MigrationName>`: The name of the migration (e.g., `InitialCreate`).
- `<MigrationsDirectory>`: The folder where migrations should be stored (relative to the `DbContext` project).
- `<StartupProject>`: The relative path to the startup project (e.g., `../Api`).


## 3. Updates
Use the following command to update the database to the latest migrations

```bash
dotnet ef database update --startup-project <StartupProject>
```

Or include the migration name to update the database to that migration.
THIS A DESTRUCTIVE OPERATION, IF YOU ROLLBACK ALL NEW DATA DEPENDING ON THE CURRENT MODEL WILL BE DESTROYED
```bash
dotnet ef database update <MigrationName> --startup-project <StartupProject>
```

To remove a migrations run the following command
The migration being removed doesn't have to be applied for this to run, if it is the migration removal will not be applied
```bash
dotnet ef database remove --startup-project <StartupProject>
```