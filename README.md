# Ticket Management System

A full-stack ticket management system built with .NET Core, Angular, and SQL Server, implementing CQRS and DDD patterns.

## Features

- Create and manage tickets
- Real-time updates with SignalR
- Automatic ticket handling after 60 minutes
- Color-coded tickets based on creation time
- Paginated ticket listing
- Responsive web interface

## Tech Stack

- **Backend**: .NET Core 8, Entity Framework Core, MediatR (CQRS), Serilog (logging)
- **Frontend**: Angular 17, TypeScript
- **Database**: SQL Server
- **Additional**: Docker, SignalR for real-time updates, Worker Service for background tasks
- **Architecture**: CQRS, DDD, Clean Architecture

## Quick Start with Docker

1. Clone the repository
2. Navigate to the project root
3. Run: `docker-compose up -d`
4. Access the application at `http://localhost:4200`

## Manual Setup

### Prerequisites

- .NET SDK 8.0+ 
- Node.js 20+ 
- SQL Server 2019+
- Docker (optional)

### Backend Setup

1. Navigate to `src/TicketManagement.API`
2. Update connection string in `appsettings.json`
3. Run migrations: `dotnet ef database update`
4. Start the API: `dotnet run`

### Frontend Setup

1. Navigate to `src/TicketManagement.Web/ClientApp`
2. Install dependencies: `npm install`
3. Start the application: `ng serve`

## API Endpoints

- `POST /api/tickets` - Create a new ticket
- `GET /api/tickets` - Get paginated tickets
- `PUT /api/tickets/{id}/handle` - Handle a ticket

## Color Coding

- **Yellow**: 15+ minutes old
- **Green**: 30+ minutes old
- **Blue**: 45+ minutes old
- **Red**: 60+ minutes old (auto-handled)

## Testing

Run unit tests: `dotnet test`

## Architecture

The solution follows Clean Architecture principles with:

- **Domain Layer**: Entities, Value Objects, Domain Services
- **Application Layer**: Use Cases, DTOs, Interfaces
- **Infrastructure Layer**: Data Access, External Services
- **Presentation Layer**: API Controllers, SignalR Hubs

<img width="1269" height="506" alt="image" src="https://github.com/user-attachments/assets/fc7b2e8e-e669-4cd6-a0b7-87d25875c4b6" />

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request
