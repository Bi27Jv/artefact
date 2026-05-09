This is my artefact for CET3011 Assignments 1&2.

Pre-requisites:
- Visual Studio 2022 or later
- .NET 8.0 SDK (Available at https://dotnet.microsoft.com/download/dotnet/8.0)
- NuGet packages (will be restored automatically when building the solution):
  - Microsoft.EntityFrameworkCore 8.0.11
  - Microsoft.EntityFrameworkCore.Sqlite 8.0.11
  - Microsoft.EntityFrameworkCore.Tools 8.0.11
  - BCrypt.Net-Next 4.1.0

Database:
The application uses SQLite to create local database files for storing user and project information.
Sensitive information such as passwords are encrypted before being stored in the database using a Bcrypt hash.

Included 'test' accounts:
- Username: test, Email: test@example.com, Password: test1234
- Username: Jack, Email: Jack@email.com, Password: Jack1234
- Username: Yongiang Cheng, Email: Yongqiang@testing.co.uk, Password: Yongqiang1234

Instructions to run the code:
1. Clone the repository to your local machine.
2. Open "Artefact.sln" in Visual Studio.
3. Press F5 to build and run the solution.
4. Follow the prompts to register an account
5. Log in with a registered account
6. Create a new project
7. Create project updates and view them in the project dashboard