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

**Endpoint: /api/login**\
Request body:
```
{
  "email": "string",
  "password": "string"
}
```
Response body:
```
{"user":
  {
    "email":"jaymel.tapel@gmail.com",
    "lastName":"Tapel",
    "firstName": "Jaymel",
    "id":"15328f47-893e-47a1-ad68-453f4a766786",
    ...
  },
  "token":
  {
    "value":"eyJhbGciOiJIUzI1NiI...",
    "expires":"11/3/2021 5:12:53 AM"
  }
}
```


**Endpoint: /api/register**
Request body:
```
{
  "email":"jaymel.tapel@gmail.com,
  "password":"1234",
  "firstName":"Jaymel",
  "lastName":"Tapel"
}
```

Response body:
```
{
  "email":"jaymel.tapel@gmail.com",
  "lastName":"Tapel",
  "firstName": "Jaymel",
  "id":"15328f47-893e-47a1-ad68-453f4a766786",
  ...
}
```


**Endpoint: /api/customers/getall** - requires JWT Bearer Token\
Request body:
```
{
  "filterKeyword": "<Any string or keyword>",
  "sortOrder": "<ASC or DESC>",
  "sortColumn": "<Id, LastName, FirstName, Email or CustCode>"
}
```
Response body:
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

**Endpoint: /api/customer/add** - requires JWT Bearer Token\
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

Response body:
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

**Endpoint: /api/customer/update** - requires JWT Bearer Token\
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
Response body:
```
{
  true // if successful
}
```

**Endpoint: /api/customer/get/7** - requires JWT Bearer Token\
Response body:
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

**Endpoint: /api/customer/delete/7** - requires JWT Bearer Token\
Response body:
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
