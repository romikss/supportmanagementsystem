# Support Management System (.NET Core 2.0)

The project consists of API and Web GUI. There is no any persistent storage and all the data is stored in memory. 
Every time the page is refreshed the "current date" is increased by one day to emulate "current date" date change.
Refresh button allows to reset "current date".

## Prerequisites

[.NET Core 2.0 SDK](https://www.microsoft.com/net/download/core) to build the project.

[Restore NuGet packages](https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore) once the project is loaded:
<br/>
`dotnet restore` from the Package Manager Console

## Live demo

You can try Support Management System here: http://supportmanagementsystem.azurewebsites.net/