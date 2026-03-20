# Simple Passenger Check In Backend

To run project:
If you do not have docker desktiop running, make sure it is installed and running.
- `docker compose up -d` - to run database in a container
- `dotnet run`

### Endpoints

http://localhost:5204/

"+"

- GET api/users
- GET api/users/{id}
- POST api/users

```
{ 
  "firstName": "testUser",
  "lastName": "User",
  "email": "testPost@mail.com",
  "dateOfBirth": "2000-05-13",
  "password": "test123"
  }
  ```

- GET api/flights?
- GET api/flights/{id}
- POST api/flights

```
{  
  "flightNumber": "RK123",
  "airline": "Lufthansa",
  "origin": "Berlin, Germany",
  "destination": "London, UK",
  "departureTime": "2026-09-10T23:40:00Z"
}
```

- GET /api/bookings?userId=1
- GET /api/bookings?flightId=10
- GET /api/bookings?userId=1&flightId=10
- GET /api/bookings/by-email?email=john@mail.com

- POST /api/bookings
```
{
  "passengerId": 2,
  "flightId":2
}
```
