using PortfolioShared.Models;

namespace PortfolioClient.Wasm.Services;

public interface IProjectService
{
    /// <summary>
    /// Gets all projects from the API
    /// </summary>
    /// <returns>List of projects or empty list on error</returns>
    Task<List<Project>> GetProjectsAsync();

    /// <summary>
    /// Gets a single project by ID
    /// </summary>
    /// <param name="id">Project ID</param>
    /// <returns>Project or null if not found</returns>
    Task<Project?> GetProjectByIdAsync(int id);
}
