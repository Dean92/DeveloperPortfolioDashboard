# Developer Portfolio Dashboard

A modern, full-stack portfolio application built with Blazor Server and ASP.NET Core Web API, showcasing professional projects and skills with an elegant, interactive user interface.

## 🚀 Features

- **Modern UI/UX**: Clean, responsive design with smooth animations and gradient effects
- **Interactive Navigation**: Single-page application experience with dynamic section switching
- **Project Showcase**: Detailed project presentations with key features, tech stacks, and GitHub links
- **Contact Section**: Professional contact information with social media integration
- **Authentication**: Secure Azure AD (Entra ID) integration with OpenID Connect
- **RESTful API**: ASP.NET Core Web API backend with Entity Framework Core
- **Database**: PostgreSQL database with Entity Framework Core migrations

## 🛠️ Tech Stack

### Frontend (PortfolioClient)

- **Blazor Server** (.NET 9.0) - Interactive server-side rendering
- **CSS3** - Custom styling with animations and gradients
- **JavaScript** - Text rotation animations and DOM manipulation
- **Azure AD Authentication** - OpenID Connect for secure access

### Backend (PortfolioAPI)

- **ASP.NET Core Web API** (.NET 9.0)
- **Entity Framework Core** - ORM for database operations
- **PostgreSQL** - Primary database
- **RESTful Architecture** - Clean API design

### Shared (PortfolioShared)

- **Models** - Project, Skill, User entities
- **DTOs** - Data transfer objects for API communication

## 📁 Project Structure

```
DeveloperPortfolioDashboard/
├── PortfolioAPI/              # Backend Web API
│   ├── Controllers/           # API endpoints
│   ├── Data/                  # DbContext and database configuration
│   ├── Migrations/            # EF Core migrations
│   └── Properties/            # Launch settings and configurations
├── PortfolioClient/           # Frontend Blazor application
│   ├── Components/
│   │   ├── Layout/            # Layout components
│   │   ├── Pages/             # Razor pages (Home, Error)
│   │   ├── ContactSection.razor
│   │   └── ProjectsSection.razor
│   ├── wwwroot/
│   │   ├── css/               # Custom CSS files
│   │   ├── images/            # Images and assets
│   │   └── portfolio.js       # JavaScript utilities
│   └── CustomAuthStateProvider.cs
└── PortfolioShared/           # Shared library
    ├── Models/                # Entity models
    └── Dtos/                  # Data transfer objects
```

## 🚦 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (for database)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Azure AD tenant (for authentication)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/Dean92/DeveloperPortfolioDashboard.git
   cd DeveloperPortfolioDashboard
   ```

2. **Configure Database Connection**

   Update `appsettings.json` in `PortfolioAPI` with your PostgreSQL connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=PortfolioDB;Username=youruser;Password=yourpassword"
     }
   }
   ```

3. **Configure Azure AD Authentication**

   Update `appsettings.json` in `PortfolioClient`:

   ```json
   {
     "EntraId": {
       "ClientSecret": "your-client-secret"
     }
   }
   ```

4. **Run Database Migrations**

   ```bash
   cd PortfolioAPI
   dotnet ef database update
   ```

5. **Build the Solution**
   ```bash
   cd ..
   dotnet build
   ```

### Running the Application

#### Option 1: Using Visual Studio

1. Open `DeveloperPortfolioDashboard.sln`
2. Set multiple startup projects (both PortfolioAPI and PortfolioClient)
3. Press `F5` to run

#### Option 2: Using Command Line

**Terminal 1 - API:**

```bash
cd PortfolioAPI
dotnet run
```

**Terminal 2 - Client:**

```bash
cd PortfolioClient
dotnet run
```

The application will be available at:

- **Client**: http://localhost:5129
- **API**: http://localhost:5000 (or as configured)

## 🎨 Features Overview

### Home Section

- Animated hero section with rotating tagline
- Professional introduction
- Call-to-action button
- Floating particle effects

### Projects Section

- Vertical layout with detailed project descriptions
- Key features and achievements with bullet points
- Technology stack badges
- GitHub and project links
- Professional project presentation

### Contact Section

- Profile image display
- Email contact
- LinkedIn integration
- GitHub profile link

## 🔒 Security

- Azure AD (Entra ID) authentication
- OAuth 2.0 and OpenID Connect
- Secure cookie-based sessions
- HTTPS enforcement in production
- CORS configuration for API security

## 📝 API Endpoints

### Projects

- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project
- `PUT /api/projects/{id}` - Update project
- `DELETE /api/projects/{id}` - Delete project

### Skills

- `GET /api/skills` - Get all skills
- `POST /api/skills` - Create new skill

### Users

- `GET /api/users` - Get all users
- `POST /api/users` - Create new user

## 🚀 Deployment

The application is configured for Azure deployment:

- **Client**: Azure App Service
- **API**: Azure App Service
- **Database**: Azure Database for PostgreSQL

Publish profiles are included in `Properties/PublishProfiles/`.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👤 Author

**Dean McCoy**

- GitHub: [@Dean92](https://github.com/Dean92)
- LinkedIn: [Dean McCoy](https://www.linkedin.com/in/dean-mccoy-8b918020b/)

## 🙏 Acknowledgments

- Inspired by modern portfolio designs
- Built with Microsoft's Blazor framework
- UI/UX patterns from contemporary web design

---

**Note**: This is a portfolio project demonstrating full-stack development capabilities with .NET technologies.
