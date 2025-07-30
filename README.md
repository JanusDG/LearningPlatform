# LearningPlatform

**LearningPlatform** is a non-commercial web application that simulates the functionality of an online course platform. Built with **ASP.NET Core MVC** and backed by an **MS SQL Server** database, the platform allows users to register, log in, and interact with course content based on their assigned roles.

## 🔧 Features

- **JWT Authentication**: Secure access with JSON Web Tokens, supporting role-based authorization.
- **Admin Interface**: Authenticated users with the **`admin`** role can:
  - Add, edit, and remove **courses**
  - Manage **users**
- **User Interface**: Authenticated users with the **`user`** role can:
  - Access their **profile page**
  - View and explore all **available courses** assigned to them

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core MVC
- **Authentication**: JWT-based role authorization
- **Database**: Microsoft SQL Server (EF Core)
- **Frontend**: Razor Views, Bootstrap CSS, JavaScript

