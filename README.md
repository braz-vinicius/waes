# Introduction

For this assignment the .Net Core framework was chosen as base platform given it's cloud and multiplatform nature. 
Since one of the non-functional requirements was scalability, the API is Docker-friendly allowing the solution to be put 
into a Kubernetes Pod for simple and effective horizontal scalability. 

# Added features:
* OpenAPI valid schema allowing easy discoverability and scaffolding of API services (http://{api}/swagger/v1/swagger.json).
* API explorer frontend to visualize and interact with the API's resources (http://{api}/swagger).
* HTTPS protocol support (using a self-signed certificate)
* Logger service infrastructure

# Assumptions:
* The id field of the diff comparison was assumed to be an Integer.


# How-to build:
* Clone the solution repository
* Open a console and go to the solution folder.
* Enter "dotnet build".

# How-to test:
* Clone the solution repository
* Open a console and go to the solution folder.
* Enter "dotnet test" .

# How-to run (Console):
* Clone the solution repository
* Open a console and go to the solution folder.
* Enter "dotnet run".
* To view all API's resources, open the browser and go to: https://localhost:5001/swagger  or http://localhost:5000/swagger.

# How-to run (Docker):
* You should have Docker installed in your system.
* Clone the solution repository
* Open a console and go to the solution folder.
* Enter "docker build -t waes-api ." into the console.
* Enter "docker run -d -p 60888:80 -p 44360:443 --name myapp waes-api" into the console.
* To view all API's resources, open the browser and go to: https://localhost:44360/swagger  or http://localhost:60888/swagger.


# Improvements to be made:

* Inject and call appropriate ILogger methods from controller actions for easy and simple tracing/debugging/auditing.
* Create a new generic interface IDiffService<TType> allowing comparisons for different types of objects.
* Create a robust CI/CD pipeline containing quality gates for tests code coverage.


Thank you for evaluating my assessment,  
Vinicius Braz
