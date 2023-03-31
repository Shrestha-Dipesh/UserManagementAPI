# UserManagementAPI

>## Prerequisites
1. .NET Core 7.0 or above
2. Visual Studio 2022 (plus ASP.NET components)
3. SQL Server Management Studio (SSMS)
\
&nbsp;
>## Getting Started
1. Go to the desired directory and run the following command in Git Bash:
```bash
git clone https://github.com/Shrestha-Dipesh/UserManagementAPI.git
```

2. Open the project in Visual Studio and replace the server name (from your SSMS) in appsettings.json.
```bash
"DefaultConnection": "Server={ServerName};Database=UserManagement;Trusted_Connection=True;Encrypt=False"
```

3. Go to Tools > NuGet Package Manager > Package Manager Console and run the following command:
```bash
Update-Database
```
