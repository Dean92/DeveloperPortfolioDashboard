using PortfolioShared.Models;

namespace PortfolioClient.Wasm.Services;

public class StaticProjectService : IProjectService
{
    private readonly ILogger<StaticProjectService> _logger;
    private readonly List<Project> _projects;

    public StaticProjectService(ILogger<StaticProjectService> logger)
    {
        _logger = logger;
        _projects = GetStaticProjects();
    }

    public Task<List<Project>> GetProjectsAsync()
    {
        _logger.LogInformation("Returning {Count} static projects", _projects.Count);
        return Task.FromResult(_projects);
    }

    public Task<Project?> GetProjectByIdAsync(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        
        if (project == null)
        {
            _logger.LogWarning("Project {ProjectId} not found", id);
        }
        else
        {
            _logger.LogInformation("Returning static project {ProjectId}", id);
        }
        
        return Task.FromResult(project);
    }

    private static List<Project> GetStaticProjects()
    {
        return new List<Project>
        {
            new Project
            {
                Id = 1,
                Title = "Hockey Rink",
                IntroDescription = "A Software as a Service (SaaS) solution for hockey rink management, streamlining operations for small businesses.",
                Description = "The frontend, built with Angular 20 and Bootstrap 5.3.3, provides an intuitive interface for users to register for sessions, process credit card payments, and manage their bookings. Features include session registration for hockey leagues, lessons, and figure skating programs with skill-level based league selection.",
                DetailedDescription = "The backend leverages .NET Core 9 API with Entity Framework Core and Azure SQL for robust data management. An administrative dashboard enables comprehensive management of leagues, sessions, teams, and payment processing. The application is deployed using GitHub Actions CI/CD pipeline to Azure Static Web Apps, ensuring reliable and scalable service delivery.",
                KeyFeatures = new List<string>
                {
                    "<strong>User Registration & Payments:</strong> Seamless user registration with integrated credit card payment processing for all session types.",
                    "<strong>Skill-Based League Management:</strong> Users can sign up for sessions matching their skill level, with automated team assignment and league organization.",
                    "<strong>Administrative Dashboard:</strong> Comprehensive backend management system for leagues, sessions, teams, and payment tracking.",
                    "<strong>Modern SaaS Architecture:</strong> Built with .NET Core 9, Angular 20, and Azure SQL, deployed via CI/CD pipeline to Azure Static Web Apps for enterprise-grade reliability."
                },
                Technologies = new List<string> { ".NET Core 9", "Angular 20", "Entity Framework Core", "Azure SQL", "Bootstrap 5.3.3", "GitHub Actions", "Azure Static Web Apps" },
                GitHubUrl = "https://github.com/Dean92/hockey-rink-website",
                ProjectUrl = "https://lively-river-0c3237510.1.azurestaticapps.net",
                StartDate = new DateTime(2025, 1, 1)
            },
            new Project
            {
                Id = 2,
                Title = "Interactive Analytics Dashboard",
                IntroDescription = "A real-time analytics platform providing healthcare data visualization through an intuitive interface.",
                Description = "Built with modern web technologies, this application features interactive Highcharts visualizations for complex healthcare data. It implements RESTful APIs for data retrieval and processing, with optimized database queries for sub-second response times.",
                DetailedDescription = "The application features server-side processing for improved performance, with a clean UI for viewing trends, generating reports, and analyzing data patterns. Designed to handle thousands of concurrent users with minimal latency.",
                KeyFeatures = new List<string>
                {
                    "<strong>Data Visualization:</strong> Implemented interactive Highcharts for comprehensive data analysis and reporting.",
                    "<strong>Performance Optimization:</strong> Optimized database queries for sub-second response times serving thousands of concurrent users.",
                    "<strong>RESTful Architecture:</strong> Built scalable APIs using C# and Entity Framework for efficient data management.",
                    "<strong>Real-Time Analytics:</strong> Provided instant data updates and dynamic filtering for enhanced user experience."
                },
                Technologies = new List<string> { "C#", "RESTful APIs", "Highcharts", "Entity Framework", "TypeScript", "SQL Server" },
                GitHubUrl = "https://github.com/Dean92",
                StartDate = DateTime.UtcNow.AddYears(-3)
            },
            new Project
            {
                Id = 3,
                Title = "Accessible UI Components",
                IntroDescription = "A suite of Angular UI components optimized for WCAG 2.0 AA accessibility standards.",
                Description = "Developed for The Joint Commission, this project focused on creating accessible UI components for healthcare compliance applications. Components were built with Angular and styled using Bootstrap and SASS, ensuring responsive design across all devices.",
                DetailedDescription = "The components feature enhanced keyboard navigation, comprehensive screen reader support, and adherence to accessibility best practices. Each component was thoroughly tested for compliance with WCAG 2.0 AA standards.",
                KeyFeatures = new List<string>
                {
                    "<strong>WCAG 2.0 AA Compliance:</strong> All components meet or exceed accessibility standards for healthcare applications.",
                    "<strong>Enhanced Navigation:</strong> Improved keyboard navigation and screen reader support for users with disabilities.",
                    "<strong>Responsive Design:</strong> Built with Bootstrap and SASS for seamless experience across all devices and screen sizes.",
                    "<strong>Component Library:</strong> Created reusable, well-documented components for use across multiple applications."
                },
                Technologies = new List<string> { "Angular", "TypeScript", "Bootstrap", "SASS", "WCAG 2.0", "Accessibility" },
                GitHubUrl = "https://github.com/Dean92",
                StartDate = DateTime.UtcNow.AddYears(-4)
            },
            new Project
            {
                Id = 4,
                Title = "Developer Portfolio Dashboard",
                IntroDescription = "A full-stack portfolio application built with Blazor and ASP.NET Core Web API.",
                Description = "This project represents a modern approach to portfolio management, featuring project management capabilities, GitHub integration, and dynamic content rendering. The application demonstrates clean architecture principles with separation of concerns and maintainable code structure.",
                DetailedDescription = "Built using Blazor for the frontend and ASP.NET Core Web API for the backend, with Entity Framework Core managing data persistence. The application showcases modern web development practices including responsive design, interactive UI components, and seamless API integration.",
                KeyFeatures = new List<string>
                {
                    "<strong>Modern Stack:</strong> Built with Blazor for interactive client-side functionality and ASP.NET Core for robust backend services.",
                    "<strong>GitHub Integration:</strong> Seamless integration with GitHub API for dynamic project information and repository data.",
                    "<strong>Clean Architecture:</strong> Implemented best practices with separation of concerns and maintainable code structure.",
                    "<strong>Responsive Design:</strong> Fully responsive interface with modern UI/UX patterns and smooth animations."
                },
                Technologies = new List<string> { "Blazor", "ASP.NET Core", "Entity Framework", "SQL Server", "C#" },
                GitHubUrl = "https://github.com/Dean92/DeveloperPortfolioDashboard",
                ProjectUrl = "https://github.com/Dean92/DeveloperPortfolioDashboard",
                StartDate = DateTime.UtcNow.AddMonths(-2)
            }
        };
    }
}
