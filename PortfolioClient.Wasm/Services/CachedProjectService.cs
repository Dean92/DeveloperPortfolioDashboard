using PortfolioShared.Models;

namespace PortfolioClient.Wasm.Services;

public class CachedProjectService : IProjectService
{
    private readonly ProjectService _projectService;
    private readonly ICacheService _cacheService;
    private readonly ILogger<CachedProjectService> _logger;

    private const string ProjectsCacheKey = "projects_all";
    private const string ProjectCacheKeyPrefix = "project_";
    private const int CacheExpirationMinutes = 5;

    public CachedProjectService(
        ProjectService projectService,
        ICacheService cacheService,
        ILogger<CachedProjectService> logger
    )
    {
        _projectService = projectService;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        // Try to get from cache first
        var cachedProjects = _cacheService.Get<List<Project>>(ProjectsCacheKey);
        if (cachedProjects != null)
        {
            _logger.LogInformation("Returning {Count} projects from cache", cachedProjects.Count);
            return cachedProjects;
        }

        // Cache miss - fetch from API
        _logger.LogInformation("Cache miss - fetching projects from API");
        var projects = await _projectService.GetProjectsAsync();

        // Cache the results
        if (projects.Any())
        {
            _cacheService.Set(ProjectsCacheKey, projects, CacheExpirationMinutes);
            _logger.LogInformation(
                "Cached {Count} projects for {Minutes} minutes",
                projects.Count,
                CacheExpirationMinutes
            );
        }

        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var cacheKey = $"{ProjectCacheKeyPrefix}{id}";

        // Try to get from cache first
        var cachedProject = _cacheService.Get<Project>(cacheKey);
        if (cachedProject != null)
        {
            _logger.LogInformation("Returning project {ProjectId} from cache", id);
            return cachedProject;
        }

        // Cache miss - fetch from API
        _logger.LogInformation("Cache miss - fetching project {ProjectId} from API", id);
        var project = await _projectService.GetProjectByIdAsync(id);

        // Cache the result
        if (project != null)
        {
            _cacheService.Set(cacheKey, project, CacheExpirationMinutes);
            _logger.LogInformation(
                "Cached project {ProjectId} for {Minutes} minutes",
                id,
                CacheExpirationMinutes
            );
        }

        return project;
    }
}
