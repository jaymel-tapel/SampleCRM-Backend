# SampleCRM-Backend
This project is built with .NET Core 5

**SampleCRM.API contains:**
- Customer Contoller
- User Controller
- ErrorHandling Middleware
- Customer Service 
- User Service 
- ViewModels
- App Settings Files

**SampleCRM.Data contains:**
- ICustomerReposity
- IUserRepository
- EntityFramework Core Repository Implementations

**SampleCRM.Models contains:**
- Application User Model
- Customer/CustomerBasicInfo Model

**SampleCRM.Helpers contains:**
- AuthSettings Configuration Model
- Token Model

### Endpoints
**/api/customers/getall** - requires JWT Bearer Token\
Request body:
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
    "email":"johndoe@gmail.com",
    "birthday":"2021-09-23T08:00:00",
    "phone":"09218945276",
    "address":"83 Sample street, Manila",
    }
]
```

**/api/customer/add** - requires JWT Bearer Token\
Request body:
```
{
  "lastName": "Doe",
  "firstName": "John",
  "email": "user@example.com",
  "birthday": "2021-09-23", // Use Date ISO String
  "phone": "string",
  "address": "string"
}
```

Response
```
{
  "id":7,
  "custCode":"johndoe20210923",
  "lastName":"Doe",
  "firstName":"John",
  "email":"johndoe@gmail.com",
  "birthday":"2021-09-23T08:00:00",
  "phone":"09218945276",
  "address":"83 Sample street, Manila",
  }
```

**/api/customer/update** - requires JWT Bearer Token\
Request body:
```
{
  "id":7,
  "lastName": "Dog",
  "firstName": "John",
  "email": "johndoe@gmail.com",
  "birthday": "string", // Use Date ISO String
  "phone": "string",
  "address": "string"
}
```
Response
```
{
  true // if successful
}
```

**/api/customer/get/7** - requires JWT Bearer Token\
Response
```
{
  "id":7,
  "custCode":"johndoe20210923",
  "lastName":"Doe",
  "firstName":"John",
  "email":"johndoe@gmail.com",
  "birthday":"2021-09-23T08:00:00",
  "phone":"09218945276",
  "address":"83 Sample street, Manila",
  }
```

**/api/customer/delete/7** - requires JWT Bearer Token\
Response
```
{
  true // if successful
}
```


## How to use
1. Packages should be automatically installed/resolved. If not, use **dotnet restore** while in a project folder or by using **--project** flag;
2. Edit **Sample appsettings.json** and provide your own AppSecret and SQLServerConnection string. Remove **Sample ** from file name.
3. Edit **Sample appsettings.development.json** and provide your own AppSecret and DEVELOPMENT SQLServerConnection string. Remove **Sample ** from file name.
4. Type **dotnet ef database update --project=SampleCRM.Data** if you are using powershell/git bash or enter **Update-Database** for Nuget Package Manager while the SampleCRM.Data is selected.  


## To be added
- Pagination for customers

## Plans in the future
- CRM.Database Project
- Dapper Repository Implementation
- Unit tests
