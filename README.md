# Chat Room Service Web API
This repository contains code of backend service for a generic chat application. See the [chat-room-app](https://github.com/marcusvx/chat-room-app) repository for a client implementation.

## Dependencies
- .NET Core 5
- MySQL

#### Main .NET Libraries

- Swashbuckle (Swagger implementation)
- AspNetCore
- Automapper
- Dapper
- Newtonsoft.Json
#### For testing
- xUnit
- Moq
## Running Locally

### Configuring Database connection
The application depends on a MySQL database. Use one of the following methods to define the database connection settings:

#### Method 1: DotNet user secrets
Open a command prompt and navigate to the `src/ChatRoomServer.WebApi/` directory. Run the following command to initialize a new user secret configuration for the current project:

```
dotnet user-secrets init
```
Then, add individual configuration values:

```
dotnet user-secrets set "DB_HOST" "<MySQL Database Host>"
dotnet user-secrets set "DB_NAME" "<MySQL Database Name>"
dotnet user-secrets set "DB_USER" "<MySQL Database User>"
dotnet user-secrets set "DB_PASSWORD" "<MySQL Database Password>"
```
User secrets reduces the risk of exposing sensitive information.

#### Method 2: Envinronment variables
The application is ready to accept environment variables for the database configuration. In the context (same terminal session) of the running application, set the following environment variables:

For MacOS X/Linux:
```
export DB_NAME="<MySQL Database Name>" 
export DB_USER="<MySQL Database User>"
export DB_HOST="<MySQL Database Host>"
export DB_PASSWORD="<MySQL Database Password>"
```
For Windows:
```
set DB_NAME=<MySQL Database Name> 
set DB_USER=<MySQL Database User>
set DB_HOST=<MySQL Database Host>
set DB_PASSWORD=<MySQL Database Password>
```

Once the database connection variables are configured you can run the project:

```
dotnet build
dotnet run --project src/ChatRoomServer.WebApi/ChatRoomServer.WebApi.csproj  
```

The application should start on http://locahost:5000.

## Running with Docker
First, create a .env file in the repository root directory. Copy and edit accordingly the file `.env.example` to get started.

From the root directory, run:
```
docker-compose up
```

The container should start both web server on http://locahost:5000 and the MySQL database.

## Seeding the database 
There is a console line utility for seeding the database with some random data. To use it, run the console application available in `tools/DataGenerator.Console`. The database connection variables should be set exactly like the ones explained above to run the web project.

```
dotnet run --project tools/DataGenerator.Console 
```
## Creating a production-ready bundle
To prepare the application for publishing, run:
```
dotnet publish -c Release -o ./publish  
```

## Run Tests

From the root directory use the following commands:

```
dotnet test
```