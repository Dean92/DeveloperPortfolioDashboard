using System.Net.Http.Json;
using PortfolioShared.Models;

namespace PortfolioClient.Services
{
    public class ProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubService _gitHubService;

        public ProjectService(IHttpClientFactory httpClientFactory, GitHubService gitHubService)
        {
            _httpClient = httpClientFactory.CreateClient("PortfolioApi");
            _gitHubService = gitHubService;
        }

        public async Task<List<Project>?> GetProjectsAsync()
        {
            try
            {
                var projects = await _httpClient.GetFromJsonAsync<List<Project>>("api/projects");
                if (projects != null)
                {
                    foreach (var project in projects)
                    {
                        if (!string.IsNullOrEmpty(project.GitHubUrl) && project.GitHubProjectNumber > 0)
                        {
                            var projectData = await _gitHubService.GetGitHubProjectDataAsync(project.GitHubUrl, project.GitHubProjectNumber);
                            if (projectData.HasValue)
                            {
                                project.GitHubProjectTitle = projectData.Value.Title;
                                project.GitHubProjectColumn = projectData.Value.ColumnName;
                            }
                        }
                    }
                }
                return projects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects: {ex.Message}");
                return null;
            }
        }
    }
}
