# AI-Auto-Blogger-API
To be paired with the ai-auto-blogger react-based web client project
## Architecture & Projects

```
WorkflowEngine.sln
├─ WorkflowEngine.Domain       # Domain models and contracts
├─ WorkflowEngine.Infrastructure # Persistence layer (EF Core + SQLite)
├─ WorkflowEngine.Core         # Application/business logic services
├─ WorkflowEngine.Api          # HTTP API + authentication + Swagger
└─ WorkflowEngine.Runtime      # In-memory node registry & execution engine
```

Each layer has a single responsibility and depends only on layers below it (Domain at the bottom, API & Runtime at the top).

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- EF Core CLI (`dotnet tool install --global dotnet-ef --version 8.*`, optional)  
- Git (to clone the repo)

---

## Build & Run

1. **Clone** the repository:
   ```bash
   git clone <repo-url>
   cd <repo-folder>
   ```
2. **Restore & build** all projects:
   ```bash
   dotnet build
   ```
3. **Configure the database** (see Infrastructure README):
   - Ensure `WorkflowEngine.Api/Data` exists
   - Update connection string in `WorkflowEngine.Api/appsettings.json`
4. **Run EF migrations**:
   ```bash
   dotnet ef migrations add InitialCreate \
     --project WorkflowEngine.Infrastructure \
     --startup-project WorkflowEngine.Api
   dotnet ef database update \
     --project WorkflowEngine.Infrastructure \
     --startup-project WorkflowEngine.Api
   ```
5. **Launch the API**:
   ```bash
   dotnet run --project WorkflowEngine.Api
   ```
6. **Browse** Swagger at `http://localhost:5015/swagger` to test endpoints.

---

## Project READMEs

Each project contains its own README with detailed instructions and design notes:

- **Domain**: Business model & contracts  
- **Infrastructure**: EF Core schema, migrations & persistence  
- **Core**: Service interfaces, DTOs, and implementations  
- **API**: REST endpoints, DI, authentication, Swagger  
- **Runtime**: Node registry & execution engine bootstrap

Refer to those for module‑specific guidance.

