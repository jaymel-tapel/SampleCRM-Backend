# SampleCRM-Backend
This project is built with .NET Core 5

## Projects/Layers

### API
- Customer Contoller
- User Controller
- ErrorHandling Middleware
- Customer Service 
- User Service 
- ViewModels

### Data
- ICustomerReposity
- IUserRepository
- EntityFramework Core Repository Implementations

### Models
- Application User Model
- Customer/CustomerBasicInfo Model

### Helpers
- AuthSettings Configuration Model
- Token Model

## How to use
1. Packages should be automatically installed/resolved. If not, use **dotnet restore** while in a project folder or by using **--project** flag;
2. Edit **Sample appsettings.json** and provide your own AppSecret and SQLServerConnection string. Remove **Sample ** from file name.

## To be added
- Pagination for customers

## Plans in the future
- CRM.Database Project
- Dapper Repository Implementation
- Unit tests
