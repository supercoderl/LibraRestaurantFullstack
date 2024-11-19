# Libra Restaurant

![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/alex289/CleanArchitecture/dotnet.yml)

The restaurant's backend server is the powerhouse that drives a smooth and efficient dining experience. Built with robust architecture and scalability in mind, it supports key functionalities, ensures data integrity, and handles all operations seamlessly.

## Project Structure
The project follows the Onion Architecture, which means that the codebase is organized into layers, with the domain model at the center and the outer layers dependent on the inner layers.

The project has the following structure:

- **Domain**: Contains the domain model, which represents the core business logic of the application. It includes entities, value objects, domain services, and domain events.
- **Application**: Contains the application layer, which implements the use cases of the system. It includes commands, queries, handlers, and DTOs.
- **Infrastructure**: Contains the infrastructure layer, which implements the technical details of the system. It includes database access, logging, configuration, and external services.
- **API**: Contains the presentation layer, which exposes the functionality of the system to the outside world. It includes controllers, action results, and view models.

## Dependencies
The project uses the following dependencies:

- **MediatR**: A lightweight library that provides a mediator pattern implementation for .NET.
- **Entity Framework Core**: A modern object-relational mapper for .NET that provides data access to the application.
- **FluentValidation**: A validation library that provides a fluent API for validating objects.
- **gRPC**: gRPC is an open-source remote procedure call framework that enables efficient communication between distributed systems using a variety of programming languages and protocols.

## Running the Project
To run the project, follow these steps:


1. Clone the repository to your local machine.
2. Open the solution in your IDE of choice.
3. Build the solution to restore the dependencies.
4. Update the connection string in the appsettings.json file to point to your database.
5. Start the API project
6. The database migrations will be automatically applied on start-up. If the database does not exist, it will be created.
7. The API should be accessible at `https://localhost:<port>/api/<controller>` where `<port>` is the port number specified in the project properties and `<controller>` is the name of the API controller.
