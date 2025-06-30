using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioShared.Models;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly PortfolioDbContext _context;

    public ProjectsController(PortfolioDbContext context)
    {
        _context = context;
    }

    [HttpGet("test-db")]
    public async Task<IActionResult> TestDatabase()
    {
        try
        {
            await _context.Database.CanConnectAsync();
            return Ok("Database connection successful.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database connection failed: {ex.Message}");
        }
    }

    // Existing endpoints (GetProjects, CreateProject, etc.) remain unchanged
}
