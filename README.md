# ECommerce Platform BE (ASP.NET Core 8 + EF Core)

A clean-architecture backend for an e-commerce system (.NET 8, EF Core, SQL Server).

## 1) Getting Started

### Prerequisites
- .NET SDK 8.x
- SQL Server (LocalDB or SQL Server Express/Standard)
- Optional: Postman/Thunder Client for API testing

### Clone & Restore
```bash
git clone https://github.com/PCHaiLam/ECommercePlatform_BE.git
cd ECommercePlatform_BE
dotnet restore
```

## 2) Configure Connection String
Update the connection string in `ECommercePlatform.API/appsettings.Development.json` (dev) or `appsettings.json` (prod-like):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER_NAME;Initial Catalog=ECommercePlatform;Integrated Security=True;Trust Server Certificate=True"
  }
}
```

Examples:
- LocalDB: `Server=(localdb)\\MSSQLLocalDB;Database=ECommercePlatform;Trusted_Connection=True;Trust Server Certificate=True`
- SQL Express: `Data Source=YOURPC\\SQLEXPRESS;Initial Catalog=ECommercePlatform;Integrated Security=True;Trust Server Certificate=True`

## 3) Database Migration
Run from the solution root (`ECommercePlatform_BE`):

```bash
# Create initial migration (only once)
dotnet ef migrations add InitialCreate -p ECommercePlatform.Infrastructure -s ECommercePlatform.API -o Data/Migrations

# Apply migrations to database
dotnet ef database update -p ECommercePlatform.Infrastructure -s ECommercePlatform.API
```

Useful commands:
```bash
# Remove last migration (before update)
dotnet ef migrations remove -p ECommercePlatform.Infrastructure -s ECommercePlatform.API

# Drop database (dangerous)
dotnet ef database drop --force -p ECommercePlatform.Infrastructure -s ECommercePlatform.API
```

## 4) Run the API
```bash
dotnet build
dotnet run --project ECommercePlatform.API
```
- Swagger UI: http://localhost:5000/swagger (or the port shown in console)

## 5) Configuration Guide

### 5.1 JWT Authentication
- Configure in `ECommercePlatform.API/appsettings*.json`:
```json
{
  "Jwt": {
    "Issuer": "YOUR_ISSUER",
    "Audience": "YOUR_AUDIENCE",
    "Secret": "YOUR_SUPER_SECRET_KEY_AT_LEAST_32_CHARS",
    "AccessTokenMinutes": 60
  }
}
```
- Code pieces are already wired in `Startup/AuthExtensions.cs` and `Program.cs`.
- Extend policies in `AddJwtAuth` as needed.

### 5.2 Payments (MoMo / VNPAY)
- Create a new section under `appsettings*.json` for each provider, e.g.:
```json
{
  "Payments": {
    "MoMo": {
      "PartnerCode": "...",
      "AccessKey": "...",
      "SecretKey": "...",
      "Endpoint": "https://..."
    },
    "VnPay": {
      "TmnCode": "...",
      "HashSecret": "...",
      "Endpoint": "https://..."
    }
  }
}
```
- Recommended: Create `IPaymentGateway` abstraction and provider implementations in `Infrastructure`.

### 5.3 File Uploads (Images)
- Options:
  - Local storage: wwwroot/uploads (use `IFormFile` + static files)
  - Cloud storage: S3/Azure Blob/Cloudinary
- Add an `Upload` section to `appsettings*.json` (root folder, allowed size/types), then implement a service (e.g., `IFileStorageService`).

### 5.4 CORS
- Default policy allows all (dev). Adjust in `Startup/CorsExtensions.cs` for production origins.

## 6) Project Structure (Clean Architecture)
- `ECommercePlatform.Core`: Entities, domain logic, validators
- `ECommercePlatform.Application`: Use cases, interfaces (placeholder)
- `ECommercePlatform.Infrastructure`: EF Core, DbContext, Configurations, Repositories (to add)
- `ECommercePlatform.API`: Presentation layer, DI, Auth, Middleware

## 7) Conventions
- Code: PascalCase properties (e.g., `CreatedAt`)
- Database: snake_case, singular table names (e.g., `order_item`), mapped via EF configurations
- Soft delete: `deleted` boolean with global filters

## 8) Troubleshooting
- SQL connection error (Named Pipes 40 / error 53):
  - Verify server name/instance
  - Ensure SQL Server is running and TCP/Named Pipes enabled
  - Try LocalDB connection string
- EF CLI missing:
  - `dotnet tool install --global dotnet-ef`
- Regenerate migration if model changed:
  - `dotnet ef migrations add <Name> -p ECommercePlatform.Infrastructure -s ECommercePlatform.API -o Data/Migrations`

## 9) Roadmap (Next)
- Add repositories/services registration via `AddInfrastructure()`
- Implement Auth endpoints (login/register/refresh)
- Integrate MoMo/VNPAY sandbox
- Add file upload service and sample controller

---
Maintained by your team. Contributions and PRs are welcome.
