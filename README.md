# Projects
* WebApi - this contains the main api and the relevant services
* xUnit - unit testing

# Requirements
* CRUD api for key-value store. ✅
* Unit tests, large data, error handling, etc. ✅

# Optional 
* User authentication using a jwt.
* Azure deploy using app service.

# Project structure
* Controllers - which is the place for the rest apis.
* Services - which is the interface for mongodb
* Models - which will contain the models for representing mongo objects.
* Exceptions - which will contain in app custom exceptions
* Dto - which is the transfer objects from/to the controller.
* Settings - which is the typed objects for appsettings.json
* Helpers - which contains helpers

## How to run?
* Clone the entire solution
* Ensure dotnet cli is installed
* Use the following command to run the project:

```
dotnet run
```
* It should open swagger api doc by default, if not it should be in http://localhost:5206/swagger/index.html