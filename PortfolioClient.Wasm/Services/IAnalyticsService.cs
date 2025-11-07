namespace PortfolioClient.Wasm.Services;

public interface IAnalyticsService
{
    /// <summary>
    /// Track a custom event in Google Analytics
    /// </summary>
    Task TrackEvent(string eventName, Dictionary<string, object>? parameters = null);

    /// <summary>
    /// Track a page view
    /// </summary>
    Task TrackPageView(string pageName, string pageLocation);

    /// <summary>
    /// Track navigation to a section
    /// </summary>
    Task TrackSectionNavigation(string sectionName);

    /// <summary>
    /// Track project view
    /// </summary>
    Task TrackProjectView(string projectName);

    /// <summary>
    /// Track external link click
    /// </summary>
    Task TrackExternalLinkClick(string linkUrl, string linkText);
}
