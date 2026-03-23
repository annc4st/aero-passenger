# Simple Passenger Check In Backend
A RESTful backend API for managing passengers, flights, and bookings.
Built with ASP.NET Core and Entity Framework, using Docker for database setup.

## Overview

## 🛠 Techstack
- ASP.NET Core (.NET v10) 
- Entity Framework Core
- SQL Server (Docker)
- xUnit (unit testing)
- Swagger / OpenAPI

To run project:
1. Start the database
Make sure Docker Desktop is running:
- `docker compose up -d` - to run database in a container

2. Build and run the API
- `dotnet build`
- `dotnet run`

3. API is available at:
Base URL: http://localhost:5204/


### Endpoints

**Users**
- GET api/users
- GET api/users/{id}
- POST api/users

```
JSON
{ 
  "firstName": "testUser",
  "lastName": "User",
  "email": "testPost@mail.com",
  "dateOfBirth": "2000-05-13",
  "password": "test123"
  }
  ```

**Flights**
- GET api/flights
- GET api/flights/{id}
- POST api/flights

```
JSON
{  
  "flightNumber": "RK123",
  "airline": "Lufthansa",
  "origin": "Berlin, Germany",
  "destination": "London, UK",
  "departureTime": "2026-09-10T23:40:00Z"
}
```

**Bookings**
- GET /api/bookings?userId=1
- GET /api/bookings?flightId=10
- GET /api/bookings?userId=1&flightId=10
- GET /api/bookings/by-email?email=john@mail.com

- POST /api/bookings
Example of POST request json body:
```
JSON
{
  "passengerId": 2,
  "flightId":2
}
```

### Testing
Run unit tests:
``` dotnet test```

### Project Structure

```
aeroWebApi.sln
├── aeroWebApi/         # Main API
├── aeroWebApi.Test/   # Unit tests
```

### Notes
- Passwords are securely hashed using PBKDF2
- Database runs in Docker container
- Designed with layered architecture 
```
Controllers → Services → Repositories → Database
           ↕
          DTOs
```
- **Controllers**: handle HTTP requests and responses
- **Services**: contain business logic
- **Repositories**: handle data access
- **DTOs** (Data Transfer Objects) are used to:
* structure API requests and responses
* avoid exposing internal entity models
* enforce clear contracts between layers

