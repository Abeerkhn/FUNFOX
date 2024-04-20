# FUNFOX

## Overview

The FUNFOX project is a web application built using ASP.NET Core 6. It serves as a Basic EdTech platform allowing administrators to manage classes, view enrolled users, and chat with users. Basic users can view available classes and enroll in them. The application follows the Clean Architecture and CQRS pattern for better scalability and maintainability.

## Features

- **User Authentication and Authorization**: Users can register, login, and access various features based on their roles and permissions.
- **Password Visibility Toggle**: Enhanced user experience with the option to toggle password visibility.
- **Multilingual Support**: Supports localization with multiple languages using Microsoft.Extensions.Localization.
- **Background Job Processing**: Utilizes Hangfire for background job processing, enabling tasks such as sending emails or performing maintenance tasks.
- **Export to Excel**: Users can export data to Excel format for easy analysis and reporting.
- **Class Management**: Administrators can view a list of classes, add new classes, and export the class list to Excel format.
- **Enrollment Management**: Administrators can view enrolled users for each class and cancel enrollments if necessary.
- **Chat Functionality with SignalR**: Users and administrators can chat with each other in real-time using SignalR.
- **CQRS Pattern**: Implements the Command Query Responsibility Segregation (CQRS) pattern for better separation of concerns and scalability.
- **Clean Architecture**: Adheres to the principles of Clean Architecture for maintaining a clean and modular codebase.

## Technologies Used

- ASP.NET Core 6
- Blazor
- Hangfire
- FluentValidation
- MudBlazor
- Microsoft.Extensions.Localization
- Microsoft.AspNetCore.Components.Authorization
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration
- SignalR

## Getting Started

### Prerequisites

- .NET 6 SDK
- Visual Studio 2022 or Visual Studio Code
- SQL Server or another supported database server

### Installation

1. Clone the repository to your local machine.
2. Open the project in your preferred development environment.
3. Set `Server.csproj` as your startup project.
4. Ensure that ports `5000` and `5001` are not in use, as they are commonly used by ASP.NET Core applications. You can check for port availability using tools like `netstat` or inspecting your system's network settings.
5. Update the connection string in the `appsettings.json` file with your database connection details.
6. Run the database migrations to create the necessary tables.
7. Build and run the project.


### Usage

1. Navigate to the login page of the application.
2. To access the admin portal, use the following credentials:
   - **Email**: portaladmin@gmail.com
   - **Password**: 12345678
3. Enter your email and password to log in.
4. Access various features of the application based on your role and permissions.
5. Export data to Excel format for analysis and reporting.
6. Manage classes, enrollments, and chat with users as an administrator.
7. View available classes and enroll in them as a basic user.

## Folder Structure

- **Server**: Contains the backend logic, including controllers, services, middleware, and other server-side code.
- **Client**: Contains the Blazor components, pages, and other client-side code for the frontend.
- **Client Infrastructure**: Contains infrastructure-related code for the Blazor client, such as HTTP clients, authentication services, and data access logic.
- **Shared**: Contains code shared between the client and server, such as models, enums, DTOs (Data Transfer Objects), and validation logic.
- **Shared Infrastructure**: Contains infrastructure-related code shared between the client and server, such as configuration settings, logging utilities, and caching mechanisms.
- **Application**: Contains application-specific logic, including use cases, command and query handlers, application services, and application-specific exceptions.
- **Domain**: Contains domain entities, value objects, domain services, and other domain-specific logic that represents the core business concepts and rules.
## Additional Features

- **Enrolled Users**: Provides a list of enrolled users and their details for administrative purposes.
- **Export to Excel**: Users can export the list of enrolled users to Excel format.
- **Class Management**: Administrators can manage classes, including adding new classes and exporting class lists to Excel format.
- **Chat Functionality with SignalR**: Real-time chat functionality allows users and administrators to communicate with each other instantly.
- **CQRS Pattern**: Utilizes the Command Query Responsibility Segregation (CQRS) pattern for better scalability and separation of concerns.
- **Clean Architecture**: Follows the principles of Clean Architecture for maintaining a modular and maintainable codebase.


## Built With

This project was built using the [FullStackHero Blazor Starter Kit](https://github.com/fullstackhero/blazor-starter-kit), which provides a solid foundation for developing full-stack web applications with .NET 6 and Blazor. The starter kit offers a comprehensive set of features and components to kickstart your development process.

