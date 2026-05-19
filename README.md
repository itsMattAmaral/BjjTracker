# BjjTracker API

A RESTful API for managing Brazilian Jiu-Jitsu schools, teachers, students, classes, and attendance — built with .NET 9, following Clean Architecture principles.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Running with Docker](#running-with-docker)
  - [Running Locally](#running-locally)
- [Authentication](#authentication)
- [API Endpoints](#api-endpoints)
  - [Auth](#auth)
  - [Schools](#schools)
  - [Teachers](#teachers)
  - [Students](#students)
  - [Classes](#classes)
  - [Attendance Requests](#attendance-requests)
- [Domain Model](#domain-model)
- [Authorization Policies](#authorization-policies)
- [Environment Variables](#environment-variables)

---

## Overview

BjjTracker is a backend API designed to streamline the management of BJJ academies. It handles user registration and authentication, school creation, teacher and student management, class scheduling, and attendance tracking with an approval flow.

---

## Architecture

The solution follows **Clean Architecture** and is split into four projects:

```
BjjTracker.sln
├── BjjTracker.Api            # Controllers, models, HTTP layer
├── BjjTracker.Application    # Use cases, commands, queries (CQRS via MediatR)
├── BjjTracker.Domain         # Entities, interfaces, exceptions, enums
└── BjjTracker.Infrastructure # EF Core DbContext, repositories, migrations
```

**Patterns used:**
- CQRS with MediatR (commands and queries separated)
- Repository pattern
- TPH (Table-Per-Hierarchy) for the User inheritance chain
- JWT Bearer authentication

---

## Tech Stack

| Layer          | Technology                              |
|----------------|-----------------------------------------|
| Framework      | .NET 9 / ASP.NET Core                   |
| ORM            | Entity Framework Core 9                 |
| Database       | PostgreSQL 16                           |
| Auth           | JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer) |
| Mediator       | MediatR 14                              |
| Password Hash  | BCrypt.Net-Next                         |
| Doc Validation | DocsBRValidator (CNPJ)                  |
| API Docs       | Swagger / Swashbuckle                   |
| Containerization | Docker + Docker Compose               |

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) and Docker Compose
- PostgreSQL 16 (if running locally without Docker)

### Running with Docker

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/BjjTracker.git
   cd BjjTracker
   ```

2. Create a `.env` file in the root directory:
   ```env
   POSTGRES_USER=bjj_tracker_user_admin
   POSTGRES_PASSWORD=your_password
   POSTGRES_DB=bjj_tracker_db
   JWT_SECRET=your_base64_encoded_jwt_secret
   ```

3. Start the services:
   ```bash
   docker compose up --build
   ```

4. The API will be available at `http://localhost:5001` and Swagger UI at `http://localhost:5001/` (root).

> Database migrations run automatically on startup.

### Running Locally

1. Configure `BjjTracker.Api/appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=bjj_tracker_db;Username=bjj_tracker_user_admin;Password=your_password"
     },
     "ApplicationSettings": {
       "JWT_Secret": "your_base64_encoded_jwt_secret"
     }
   }
   ```

2. Apply migrations:
   ```bash
   dotnet ef database update --project BjjTracker.Infrastructure --startup-project BjjTracker.Api
   ```

3. Run the API:
   ```bash
   dotnet run --project BjjTracker.Api
   ```

4. Navigate to `http://localhost:5111` to access Swagger UI.

---

## Authentication

The API uses **JWT Bearer tokens**. To authenticate:

1. Register a user via `POST /register`.
2. Login via `POST /login` to receive a token.
3. Include the token in subsequent requests:
   ```
   Authorization: Bearer <your_token>
   ```

---

## API Endpoints

### Auth

| Method | Endpoint    | Description           | Auth Required |
|--------|-------------|-----------------------|---------------|
| POST   | `/register` | Register a new user   | No            |
| POST   | `/login`    | Login and get a token | No            |

### Schools

| Method | Endpoint                              | Description              | Policy       |
|--------|---------------------------------------|--------------------------|--------------|
| GET    | `/school`                             | Search schools (paged)   | Authenticated |
| GET    | `/school/{schoolId}`                  | Get school by ID         | Authenticated |
| GET    | `/school/document/{document}`         | Get school by CNPJ       | Authenticated |
| POST   | `/school`                             | Register a new school    | TeacherOnly  |
| PATCH  | `/school/{schoolId}/AddOwner`         | Add an owner to a school | SchoolOwner  |
| PATCH  | `/school/{schoolId}/RemoveOwner`      | Remove an owner          | SchoolOwner  |

### Teachers

| Method | Endpoint                              | Description              | Policy       |
|--------|---------------------------------------|--------------------------|--------------|
| GET    | `/teacher`                            | Search teachers (paged)  | Authenticated |
| GET    | `/teacher/{teacherId}`                | Get teacher by ID        | Authenticated |
| PATCH  | `/teacher/{teacherId}`                | Update teacher name      | TeacherOnly  |
| POST   | `/teacher/{teacherId}/graduateStudent`| Graduate a student       | TeacherOnly  |

### Students

| Method | Endpoint                              | Description              | Policy       |
|--------|---------------------------------------|--------------------------|--------------|
| GET    | `/student`                            | Search students (paged)  | Authenticated |
| GET    | `/student/{studentId}`                | Get student by ID        | Authenticated |
| PATCH  | `/student/{studentId}/school`         | Update student's school  | StudentOnly  |
| PATCH  | `/student/{studentId}/name`           | Update student name      | StudentOnly  |

### Classes

| Method | Endpoint            | Description              | Policy      |
|--------|---------------------|--------------------------|-------------|
| GET    | `/class`            | Search classes (paged)   | TeacherOnly |
| GET    | `/class/{classId}`  | Get class by ID          | TeacherOnly |
| POST   | `/class`            | Register a new class     | TeacherOnly |

### Attendance Requests

| Method | Endpoint                                                  | Description                   | Policy       |
|--------|-----------------------------------------------------------|-------------------------------|--------------|
| GET    | `/attendancerequest`                                      | Search all requests (paged)   | Authenticated |
| GET    | `/byClassId/{classId}`                                    | Get requests by class         | Authenticated |
| GET    | `/byStudentId/{studentId}`                                | Get requests by student       | Authenticated |
| GET    | `/byClassId/{classId}/byStudentId/{studentId}`            | Get a specific request        | Authenticated |
| POST   | `/attendancerequest`                                      | Register an attendance request | Authenticated |
| PATCH  | `/attendancerequest/Approve`                              | Approve an attendance request | TeacherOnly  |
| DELETE | `/attendancerequest`                                      | Delete an attendance request  | Authenticated |

---

## Domain Model

```
User (abstract)
├── Teacher
│   ├── IsSchoolOwner
│   └── SchoolOwned → School
└── Student
    └── ClassesAttended

School
├── Owners   → List<Teacher>
├── Teachers → List<Teacher>
├── Students → List<Student>
└── Classes  → List<Class>

Class
├── Teacher          → Teacher
├── School           → School
└── AttendanceRequests → List<AttendanceRequest>

AttendanceRequest
├── Student → Student
├── Class   → Class
└── Attended (bool)
```

**Belt progression** (via `GraduateStudent`):
`White → Grey → Yellow → Orange → Blue → Purple → Brown → Black`

---

## Authorization Policies

| Policy       | Role Required  | Description                          |
|--------------|----------------|--------------------------------------|
| TeacherOnly  | Teacher        | Actions only teachers can perform    |
| StudentOnly  | Student        | Actions only students can perform    |
| SchoolOwner  | SchoolOwner    | Actions only school owners can perform |

> A Teacher marked as `IsSchoolOwner` automatically receives the `SchoolOwner` claim in their JWT.

---

## Environment Variables

| Variable            | Description                        | Example                                |
|---------------------|------------------------------------|----------------------------------------|
| `POSTGRES_USER`     | PostgreSQL username                | `bjj_tracker_user_admin`               |
| `POSTGRES_PASSWORD` | PostgreSQL password                | `strongpassword`                       |
| `POSTGRES_DB`       | PostgreSQL database name           | `bjj_tracker_db`                       |
| `JWT_SECRET`        | Base64 encoded JWT signing key     | `a2FqZGZs...` (min. 32 chars decoded) |

---

## License

This project is for educational and personal use.
