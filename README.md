# Smart City Incident Management System

![CI](https://github.com/ivangaydazhiev/SmartCityIncidents/actions/workflows/ci.yml/badge.svg)

A backend REST API for managing city incidents such as fires, traffic accidents, and emergencies.

Built with **.NET 8** using **Clean Architecture** principles.

---

## Features

- Create, read, update, delete incidents (CRUD)
- Pagination support
- Validation with FluentValidation
- JWT Authentication 
- Role-based Authorization (Admin/User)
- Structured logging with Serilog
- Unit tests (xUnit + Moq)

---

## Architecture

This project follows **Clean Architecture**:

- **SmartCity.Api** → Controllers (HTTP layer)
- **SmartCity.Application** → Business logic (Services, DTOs, Validators)
- **SmartCity.Domain** → Entities & Enums
- **SmartCity.Infrastructure** → Database & Repositories

## Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- FluentValidation
- JWT Authentication
- Serilog
- xUnit & Moq
- Docker
- GitHub Actions(CI/CD)


## Getting Started

### 1. Clone the repository
 
```bash
git clone https://github.com/ivangaydazhiev/SmartCityIncidents.git
cd SmartCityIncidents
```

### 2. Apply database migrations

```bash
dotnet ef database update --project SmartCity.Infrastructure --startup-project SmartCity.Api
```

### 3. Run the application

```bash
dotnet run --project SmartCity.Api
```

### 4. Open Swagger UI

```
https://localhost:7072/swagger
```

---

## Run with Docker

Build the Docker image:

```bash
docker build -t smartcity-api .
```

Run the container:

```bash
docker run -d -p 8080:80 smartcity-api
```

Open in browser:

```
http://localhost:8080/swagger
```





## Authentication

### Login

**POST** `/api/Auth/login`

Request body:

```json
{
    "username": "admin",
    "password": "1234"
}
```

---

```md
### Using JWT Token

After login, copy the token and click **Authorize** in Swagger.

Enter:

Bearer YOUR_TOKEN_HERE
```

---

## API Endpoints

### Incidents

| Method | Endpoint | Description |
|--------|----------|------------|
| GET | /api/Incidents | Get all incidents |
| GET | /api/Incidents/{id} | Get incident by ID |
| GET | /api/Incidents/paged?page=1&pageSize=10 | Get paginated incidents |
| POST | /api/Incidents | Create a new incident |
| PUT | /api/Incidents/{id} | Update an incident |
| DELETE | /api/Incidents/{id} | Delete an incident |

---

## Testing 

Run all unit tests:

```bash
dotnet test
```

## Logging

Logging is implemented using **Serilog** and includes:

- HTTP requests
- Status codes
- Execution time

## Future Improvements

- Integration tests
- Caching (Redis)
- CI/CD deployment (Azure / Render)
