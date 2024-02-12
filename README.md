# Microservices.Posts 
This repository has coded using Domain Driven Design best practices and CQRS and Event Sourcing patterns with not using any CQRS framework, but this is a custom CQRS and Event Sourcing framework using C# and Apache Kafka.

## INSTALL
### Option1: 
Open Visual Studio and debug run

### Option2: 
Open terminal where is docker-compose.yml file and execute this command: 
  - docker-compose up

## Features
- Replay the event store and recreate the state of the aggregate.
- Separate read and write concerns.
- Structure your code using Domain-Driven-Design best practices.
- Replay the event store to recreate the entire read database.
- Replay the event store to recreate the entire read database into a different database type - PostgreSQL.
