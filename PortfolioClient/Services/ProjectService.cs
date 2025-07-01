using System.Net.Http.Json;
using PortfolioShared.Models;

namespace PortfolioClient.Services
{
    public class ProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PortfolioApi");
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Project>>("api/projects") ?? new List<Project>();

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching projects: {ex.Message}");
                return new List<Project>();
            }
        }

        public async Task<Project?> GetProjectAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Project>($"api/projects/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching project with ID {id}: {ex.Message}");
                return null;
            }

        }
    }
}
