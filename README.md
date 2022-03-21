# Simple Car Web API

![Simple Car Web API Screenshot](https://user-images.githubusercontent.com/60667206/159291300-a4e98cdd-c9a1-42fc-ac91-5587468885d4.png)

1. [Summary](#summary)
    1. [Key Notes](#key-notes)
    2. [Tech Stack](#tech-stack)
2. [Getting Started](#getting-started)
    1. [Requirements](#requirements)
    2. [Steps](#steps)
3. [Design Pattern Choices](#design-pattern-choices)
    1. [Repository Pattern](#repository-pattern)
    2. [CQS Pattern](#cqs-pattern)

## Summary

A simple, self-contained web api to demonstrate how to perform api commands and queries which manipulates different text files. This concept can easily be translated and configured to a local or live database .

### Key Notes

- API documentation is automatically generated using [Swagger](https://swagger.io/).
- Cross-platform with dotnet core
- Data is contained in text files, attached to the project in the 'Data/' folder.
- Project runs locally, no configuration needed.
- Can perform CRUD Actions.

### Tech Stack

1. ASP.NET Core WebAPI

## Getting Started

### Requirements

- [Visual Studio](https://visualstudio.microsoft.com/vs/community/)
- [.NET 5.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

### Steps

1.  [Clone](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository#cloning-a-repository)  or [Fork](https://docs.github.com/en/get-started/quickstart/fork-a-repo#forking-a-repository) the repo.
2.	In the downloaded folder, double-click the 'CarWebAPI.sln' file to open the solution.
3. Run the solution by clicking the green arrow 'CarWebAPI'.

## Design Pattern Choices
 
### Repository Pattern

The [Repository Pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design#the-repository-pattern) was chosen to decouple the data access layer from domain model layer. This decoupling helps remove any dependencies, repitition of logic and enforces separation. This is done through Dependency Injection in which the repositories are only injected into the required domain layers.

The Service layer was excluded from this project, as the service layer adds an extra layer between the Domain and Repository layer. This layer esssentially duplicates the Respostory layer, which requires duplication of code for each new action or query in the repository. Including the service layer will slow down development and require more effort in adding new logic to the repository layer.

### CQS Pattern

The [CQS Pattern](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation) was chosen to seperate the action logic (Create, Update, Delete) from the query logic (Read). This seperates the logic of concern and makes it easier to identify and use. New logic for can be easily added by identifying whether it is a command or query, simplifying the development process. With this seperation it also makes it easier to enforce restricted access for the command logic. 
