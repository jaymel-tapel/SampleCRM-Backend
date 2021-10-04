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

### Endpoints


**/api/customers/getall**\
Request:
```
{
  "filterKeyword": "string", // Any string or keyword
  "sortOrder": "string", // ASC or DESC
  "sortColumn": "string" // Id, LastName, FirstName, Email or CustCode
}
```

Response
```
[
  {
    "id":7,     
    "lastName":"Doe",
    "firstName":"John",
    "custCode":"johndoe20210923",
    "email":"email@gmail.com",
    "birthday":"2021-09-23T08:00:00",
    "phone":"09218945276",
    "address":"83 Sample street, Manila",
    }
]
```



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
3. Type **dotnet ef database update --project=SampleCRM.Data** if you are using powershell/git bash or enter **Update-Database** for Nuget Package Manager while the SampleCRM.Data is selected.  


## To be added
- Pagination for customers

## Plans in the future
- CRM.Database Project
- Dapper Repository Implementation
- Unit tests
