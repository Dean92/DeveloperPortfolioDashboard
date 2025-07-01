using Microsoft.AspNetCore.Components;
using PortfolioShared.Models;
using PortfolioClient.Services;


namespace PortfolioClient.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public required ProjectService ProjectService { get; set; }

        private List<Project>? projects;

        protected override async Task OnInitializedAsync()
        {

            try
            {
                projects = await ProjectService.GetProjectsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Home component: {ex.Message}");
                projects = new List<Project>();
            }
        }
    }
}
