# Product API (.NET 8.0)

This is a RESTful Web API built with .NET 8.0 that integrates with the external mock API at [https://restful-api.dev/objects](https://restful-api.dev/objects). It enables enhanced functionality such as:

- Retrieving products with filtering and pagination
- Adding new products
- Deleting existing products

---

## ?? Features

- Integration with an external RESTful API
- Filtering by product name (case-insensitive substring match)
- Pagination support
- Model validation using data annotations
- Centralized error handling
- Configurable base URL via `appsettings.json`

---

## ?? Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Setup Instructions

1. **Clone the Repository**

```bash
git clone https://github.com/Ekwughafranklin/TrueStory.git
cd product-api
