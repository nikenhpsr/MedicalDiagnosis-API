# MedicalDiagnosis-API

Requirement: Make sure you've already installed .NET SDK CLI

1. Open Visual Studio. Create New Project -> Choose C# MVC Web App

2. Install necessary packages. The packages must be compatible with each other

```
dotnet add package Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter --version 8.0.1
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore --version 8.0.1
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.1
dotnet add package Microsoft.AspNetCore.Identity.UI --version 8.0.1
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.1
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.1
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
```

3. Define GlobalUsing.cs:

```
global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using MedicalDiagnosis_API.Models;
```

4. Create your Models in Models folder.
5. Create the DbContext (in /data/) to connect your Web App to Database
6. Install the dotnet-ef tool:

```
dotnet tool install --global dotnet-ef
```

7. Configure your .NET environment. You can change the appsettings.json.

```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=YourServer;database=YourDatabase;user=YourUser;password=YourPassword"
  }
}
```

8. Configure MySQL to your Program.cs

```
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MedicalDiagnosisContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
```

9. Initialize your first migration

```
dotnet ef migrations add InitialCreate
```

10. (Optional) update your database:

```
dotnet ef database update
```

11. Add Swagger into your Program.cs:

```
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});



app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

```

12. You can run `dotnet watch run` to show dev mode. Then go into /swagger
13. Write your Controllers in /Controllers folder
14. The server should restart everytime you save a changed to a Controller
