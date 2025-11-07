using Microsoft.JSInterop;

namespace PortfolioClient.Wasm.Services;

public class GoogleAnalyticsService : IAnalyticsService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<GoogleAnalyticsService> _logger;
    private readonly IConfiguration _configuration;

    public GoogleAnalyticsService(
        IJSRuntime jsRuntime,
        ILogger<GoogleAnalyticsService> logger,
        IConfiguration configuration
    )
    {
        _jsRuntime = jsRuntime;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task TrackEvent(string eventName, Dictionary<string, object>? parameters = null)
    {
        try
        {
            if (parameters == null)
            {
                await _jsRuntime.InvokeVoidAsync("gtag", "event", eventName);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("gtag", "event", eventName, parameters);
            }

            _logger.LogDebug("Tracked event: {EventName}", eventName);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to track event: {EventName}", eventName);
        }
    }

    public async Task TrackPageView(string pageName, string pageLocation)
    {
        try
        {
            var measurementId = _configuration["GoogleAnalytics:MeasurementId"];

            await _jsRuntime.InvokeVoidAsync(
                "gtag",
                "config",
                measurementId,
                new { page_title = pageName, page_location = pageLocation }
            );

            _logger.LogDebug("Tracked page view: {PageName}", pageName);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to track page view: {PageName}", pageName);
        }
    }

    public async Task TrackSectionNavigation(string sectionName)
    {
        await TrackEvent(
            "section_navigation",
            new Dictionary<string, object>
            {
                { "section_name", sectionName },
                { "timestamp", DateTime.UtcNow.ToString("o") },
            }
        );
    }

    public async Task TrackProjectView(string projectName)
    {
        await TrackEvent(
            "project_view",
            new Dictionary<string, object>
            {
                { "project_name", projectName },
                { "timestamp", DateTime.UtcNow.ToString("o") },
            }
        );
    }

    public async Task TrackExternalLinkClick(string linkUrl, string linkText)
    {
        await TrackEvent(
            "external_link_click",
            new Dictionary<string, object>
            {
                { "link_url", linkUrl },
                { "link_text", linkText },
                { "timestamp", DateTime.UtcNow.ToString("o") },
            }
        );
    }
}
