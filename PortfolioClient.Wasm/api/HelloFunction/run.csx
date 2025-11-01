#r "Microsoft.Azure.WebJobs"
#r "Microsoft.Azure.WebJobs.Extensions.Http"
#r "System.Net.Http"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
    ILogger log)
{
    log.LogInformation("HelloFunction triggered.");
    return new OkObjectResult("Hello from Azure Function!");
}
