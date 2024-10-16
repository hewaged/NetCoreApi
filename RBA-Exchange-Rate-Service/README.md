# Microservice Architecture in ASP.NET Core

This demo shows a simple example of the microservices architecture using ASP.NET Core 6. It includes instructions on building microservices and building API gateways with [Ocelot](https://github.com/ThreeMammals/Ocelot), and how to utilise [MassTransit](http://masstransit-project.com) as distributed application framework, [RabbitMQ](https://www.rabbitmq.com) as the message broker and how to unit test microservices using [xUnit](https://xunit.net). And it also shows how to deploy microservices using [Docker](https://www.docker.com/resources/what-container) containers on Linux distributions.

## Introduction

Microservices architecture is a method of developing software systems that structures an application as a collection of loosely coupled services, each focusing on a single function or business capability. Each service operates within a discrete, confined context, communicating with other services through well-defined interfaces. Microservices do not know about each other and the only coupling between them is the standalone contract project that both references.

An API gateway accepts API requests from a client, processes them based on defined policies, directs them to the appropriate services, and combines the responses for a simplified user experience.

## Advantages of Microservices architecture

* Services are self-deployable
* Services are easy to update and testable
* Services can be deployed in multiple servers, and cloud environments to enhance performance and scalability
* Failure in one service does not impact other services
* It's easier to manage bug fixes and feature releases
* Developers can better understand the functionality of a service
 
## Solution Architecture
* **Exchange.Rates.RBA.Gateway**: Gateway to all APIs for RBA exchange rate endpoints
* **Exchange.Rates.Aud.OpenApi**: Daily exchange rate data from the Reserve Bank of Australia (RBA)
* **Exchange.Rates.Aud.Polling.Api**: Service to retrieve RBA exchange rates. It uses [RBA Daily Exchange Rate RSS feed](https://www.rba.gov.au/rss/rss-cb-exchange-rates.xml).

Each service is hosted in it's own Docker container (take a look into docker-compose project).
  
## Setup the Containers
To execute docker-compose file, open Powershell, and navigate to the docker-compose file in the root folder. Then execute the following command: **docker-compose up -d --build --remove-orphans**. The -d parameter executes the command detached. This means that the containers run in the background and don’t block your Powershell window.
To check all running Containers use **docker ps**.
 
## Swagger UI for [Exchange.Rates.Aud.OpenApi](https://localhost:5001/swagger/index.html)
 

## Call Gateway
To access **Aud Exchange Rate API** through the API Gateway:
**[https://localhost:8001/exchangeratesaud/audbaserates?symbols=NZD,USD,SGD](https://localhost:8001/exchangeratesaud/audbaserates?symbols=NZD,USD,SGD)**

## Testing
Test project includes some unit tests.

## Tools and Frameworks required
- [Visual Studio](https://www.visualstudio.com/vs/community) 2022 17.8 or greater
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/resources/what-container)

## Technologies and Frameworks 
- [.NET 8](https://github.com/dotnet/core/blob/main/release-notes/8.0)
- [Docker](https://www.docker.com/resources/what-container)  
- [Ocelot](https://github.com/ThreeMammals/Ocelot)  
- [MassTransit](http://masstransit-project.com)  
- [RabbitMQ](https://www.rabbitmq.com)  
- [OpenAPI](https://swagger.io/specification/)
- [Serilog](https://serilog.net/)
- [xUnit](https://xunit.net)
- [Implement HTTP call Retry with Http.Polly](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
- [FluentAssertions](https://fluentassertions.com/)

## A number of future enhancements can be made to this solution. Below is a list just to name a few:
- Add identity and auth
- Add persistence data storage
- Add integration tests and more unit tests
- Add load balancing


## Licence
Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
