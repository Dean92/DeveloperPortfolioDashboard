using System.Net.Http.Json;
using PortfolioShared.Models;

namespace PortfolioClient.Wasm.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(HttpClient httpClient, ILogger<ProjectService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching projects from API");

            var projects = await _httpClient.GetFromJsonAsync<List<Project>>("api/projects");

            if (projects == null)
            {
                _logger.LogWarning("API returned null for projects");
                return GetFallbackProjects();
            }

            _logger.LogInformation(
                "Successfully fetched {Count} projects from API",
                projects.Count
            );
            return projects;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error fetching projects from API");
            return GetFallbackProjects();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching projects");
            return GetFallbackProjects();
        }
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Fetching project {ProjectId} from API", id);

            var project = await _httpClient.GetFromJsonAsync<Project>($"api/projects/{id}");

            if (project == null)
            {
                _logger.LogWarning("Project {ProjectId} not found", id);
            }

            return project;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error fetching project {ProjectId}", id);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching project {ProjectId}", id);
            return null;
        }
    }

    private List<Project> GetFallbackProjects()
    {
        _logger.LogInformation("Using fallback projects");

        return new List<Project>
        {
            new Project
            {
                Id = 1,
                Title = "Developer Portfolio Dashboard",
                IntroDescription = "A comprehensive portfolio management system",
                DetailedDescription =
                    "Full-stack application built with ASP.NET Core Web API and Blazor WebAssembly, featuring Azure AD authentication, PostgreSQL database, and modern responsive UI.",
                KeyFeatures = new List<string>
                {
                    "Blazor WebAssembly SPA with interactive UI",
                    "ASP.NET Core Web API backend",
                    "Azure AD (Entra ID) authentication",
                    "Entity Framework Core with PostgreSQL",
                    "Responsive design with CSS animations",
                },
                Technologies = new List<string>
                {
                    "C#",
                    "Blazor WASM",
                    "ASP.NET Core",
                    "PostgreSQL",
                    "Azure AD",
                    "Entity Framework Core",
                },
                ProjectUrl = "https://github.com/Dean92/DeveloperPortfolioDashboard",
                StartDate = DateTime.UtcNow.AddMonths(-2),
            },
            new Project
            {
                Id = 2,
                Title = "E-Commerce Platform",
                IntroDescription = "Modern online shopping solution",
                DetailedDescription =
                    "Scalable e-commerce platform with product catalog, shopping cart, payment integration, and admin dashboard.",
                KeyFeatures = new List<string>
                {
                    "Product catalog with search and filtering",
                    "Shopping cart and checkout flow",
                    "Stripe payment integration",
                    "Admin dashboard for inventory management",
                    "Order tracking and history",
                },
                Technologies = new List<string>
                {
                    "ASP.NET Core",
                    "React",
                    "SQL Server",
                    "Stripe API",
                    "Redis",
                },
                ProjectUrl = "#",
                StartDate = DateTime.UtcNow.AddMonths(-6),
            },
            new Project
            {
                Id = 3,
                Title = "Task Management System",
                IntroDescription = "Collaborative project management tool",
                DetailedDescription =
                    "Real-time task management application with team collaboration features, drag-and-drop kanban boards, and progress tracking.",
                KeyFeatures = new List<string>
                {
                    "Kanban board with drag-and-drop",
                    "Real-time updates with SignalR",
                    "Team collaboration and assignments",
                    "Sprint planning and tracking",
                    "Activity timeline and notifications",
                },
                Technologies = new List<string>
                {
                    "ASP.NET Core",
                    "SignalR",
                    "Vue.js",
                    "PostgreSQL",
                    "Docker",
                },
                ProjectUrl = "#",
                StartDate = DateTime.UtcNow.AddMonths(-8),
            },
            new Project
            {
                Id = 4,
                Title = "Analytics Dashboard",
                IntroDescription = "Real-time data visualization platform",
                DetailedDescription =
                    "Business intelligence dashboard with interactive charts, custom reports, and data export capabilities.",
                KeyFeatures = new List<string>
                {
                    "Interactive charts with Chart.js",
                    "Custom report builder",
                    "Real-time data updates",
                    "Export to PDF/Excel",
                    "Role-based access control",
                },
                Technologies = new List<string>
                {
                    "ASP.NET Core",
                    "Angular",
                    "MongoDB",
                    "Chart.js",
                    "Azure",
                },
                ProjectUrl = "#",
                StartDate = DateTime.UtcNow.AddMonths(-4),
            },
        };
    }
}
