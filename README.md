Test task at the position Junior .Net Developer
Consist of 2 projects:
  - DogsHouse.API
  - DogsHouse.UnitTests
    
In the REST API : we have some layers (core, infrastrucute, application);
  - Application : directories Services, QueryExtensions
  - Core : directories Entities, Dtos, Mapping
  - Infrastructure: directories Migrations, Infrastrucute
    
In the Tests : 1 test class of DogService, and start example UnitTests1

The following dependencies(packages) were connected:
  - Swashbuckle.AspNetCore - default packages when we created project ASP.NET API
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Design
  - Microsoft.EntityFrameworkCore.InMemory
  - Microsoft.EntityFrameworkCore.Relational
  - Npgsql.EntityFrameworkCore.PostgreSQL
  - Npgsql
  - AutoMapper.Extensions.Microsoft.DependencyInjection
  - NUnit
  - Moq

In the project we have the main service that called DogService;
There are three actions:
  - public Task<Dog> AddAsync(DogAddDto dog, CancellationToken cancellationToken); 'should have an action that allows creating dogs as a result, a new dog should be created'
  -  public Task<IEnumerable<DogDto>> ListAsync(DogQueryParameter? parameters, CancellationToken cancellationToken); 'should return the list of the dogs'
And additional service PingService:
- public string Ping(); 'ping should return the following message: "Dogshouseservice.Version1.0.1"'

Please think about the following cases:
  -	Dog with the same name already exists in DB. (was added ID property)
  -	Tail height is a negative number or is not a number. (was added CK)
  -	Invalid JSON is passed in a request body. (was handled via try..catch block)
  -	Other cases that need to be handled in order for API to work properly. (was added rate limitter, PermitLimit = 10 with ten seconds, and 2 Http-requests in queue limit)
    
  Coverage statistic
   
  ![image](https://github.com/user-attachments/assets/394b5fe5-e77e-4a77-90ac-d0aacb0994f7)

