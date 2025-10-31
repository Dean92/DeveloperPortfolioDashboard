using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace PortfolioClient.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient for API calls
            var apiBaseUrl = builder.Configuration["PortfolioApi:BaseUrl"]
                ?? "https://portfoliodashboardapi-b4bfeje0cwdeenh8.centralus-01.azurewebsites.net/";

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl)
            });

            await builder.Build().RunAsync();
        }
    }
}