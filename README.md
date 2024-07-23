# Badminton Events Web App

Badminton Events is a webpage dedicated to providing information about badminton clubs and tournaments, inspired by [RunGroop](https://github.com/teddysmithdev/RunGroop). 

This is implemented using ASP.NET Core MVC 2022
- .NET 8
- Microsoft Identity for authentication
- Entity Framework for database management
- Image service via Cloudinary API or local hosting

## Get started

1. Create database through EF

   - Open "Package Manager Console" from Visual Studio 2022 -> View -> Other Windows
   - Change directory to proj folder
   - `Add-Migration Initial`
   - `Update-Database`

2. Populate seed data for testing

   - Open Developer Powershell
   - `dotnet run seeddata`

3. Register [Cloudinary](https://cloudinary.com/) (free) for the image storage and replace the following fields in appsettings.json accordingly

```
  "CloudinarySettings": {
    "CloudName": "",
    "ApiKey": "",
    "ApiSecret": ""
  },
```
