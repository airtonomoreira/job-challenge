This repository contains the solution for a technical challenge in .NET and React. It includes the necessary code and resources to build and deploy the application with two different folders: the application frontend and the backend develop in .NET and React. It includes the necessary code and resources to build and deploy the application.

-

# job-challenge

# Como subir o projeto para o Git

### Running the Project with Docker

To build and start all containers:

```bash:terminal
docker-compose up --build
```

The --build flag ensures that Docker rebuilds all service images, which is useful when:

- Making changes to Dockerfile
- Updating dependencies
- First time running the project
- Ensuring latest code changes are included

The services will be ready when all containers finish starting up. You can view the logs in real-time since this runs in attached mode.

To run in detached mode, add the -d flag:

```bash:terminal
docker-compose up --build -d
```

To stop the containers, run:

````bash:terminal docker-compose down

# Backend API Application

## Technology Stack
- .NET 8.0
- Entity Framework Core
- SQL Server
- Docker

## Project Structure
```/Controllers``` - REST API endpoints
```/Models``` - Domain entities and DTOs
```/Services``` - Business logic implementation
```/Data``` - Database context and configurations

## Running the Application

Build and start the backend service:

```bash:terminal
docker-compose up --build backend
````

The API will be available at `http://0.0.0.0:5179`

## Development

To run locally for development:

```bash:terminal
dotnet restore
dotnet run
```

## API Endpoints

The following endpoints are available:

- GET /api/products - List all products
- POST /api/products - Create new product
- GET /api/products/{id} - Get product by ID
- PUT /api/products/{id} - Update product
- DELETE /api/products/{id} - Delete product

## Database

The application uses SQLite as the database. Migrations are automatically applied on startup.

## Environment Variables

- `ConnectionStrings__DefaultConnection`: Database connection string
- `ASPNETCORE_ENVIRONMENT`: Runtime environment (Development/Production)

# Frontend React Application

## Technology Stack

- React 18
- TypeScript
- Material UI
- Axios
- React Query

## Project Structure

`/src/components` - Reusable UI components
`/src/pages` - Application pages/routes
`/src/services` - API integration and services
`/src/hooks` - Custom React hooks
`/src/types` - TypeScript type definitions

## Running the Application

Start the frontend service:

```bash:terminal
docker-compose up --build frontend
```

The application will be available at `http://localhost:3000`

## Development

To run locally for development:

```bash:terminal
npm install
npm start
```

## Features

- Product listing with search and filters
- Product details view
- Create/Edit product forms
- Responsive design
- Real-time data updates

## Available Scripts

- `npm start` - Runs development server
- `npm build` - Creates production build
- `npm test` - Runs test suite
- `npm lint` - Runs linter

## Environment Variables

- `REACT_APP_API_URL`: Backend API URL
- `REACT_APP_ENV`: Runtime environment

# Employee Management Application

A full-stack application built with .NET 8.0 backend and React/Next.js frontend for managing employee data.

## Architecture

### Backend (.NET 8.0)

- **API Layer**: REST API built with ASP.NET Core
- **Data Layer**: Entity Framework Core with SQLite database
- **Authentication**: JWT Bearer token implementation
- **Swagger/OpenAPI**: API documentation and testing interface
- **Serilog**: Structured logging
- **Domain-Driven Design**: Clear separation of concerns with entities, interfaces, and services

Key components:

- User management
- Employee management
- Database mappings for Users and Employees tables
- Cross-cutting concerns like mapping profiles

### Frontend (Next.js)

- Modern React application with TypeScript
- Built on Next.js framework for optimal performance
- Auto-updates during development
- Font optimization with next/font
- Development server runs on port 3000

## Running the Application

### Using Docker (Recommended)

```bash
docker-compose up --build
```

### Local Development

Backend:

```bash
cd backend
dotnet restore
dotnet run
```

Frontend:

```bash
cd frontend
npm install
npm run dev
```

## Database

- SQLite database (employee.db)
- Automatic migrations handled by Entity Framework Core
- Detailed error logging and sensitive data logging enabled for development

## Features

- User authentication and authorization
- Employee CRUD operations
- Secure API endpoints
- Swagger documentation
- Responsive UI
- Real-time data updates

## API Documentation

Access the Swagger documentation at `/swagger` endpoint when running the backend application.

## Development Notes

- Backend migrations assembly: Api.Application
- Entity configurations using Fluent API
- Structured logging implementation
- JWT authentication support
