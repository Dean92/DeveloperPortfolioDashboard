using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection;
using PortfolioClient.Components;
using PortfolioClient;
using PortfolioClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add data protection services
builder.Services.AddDataProtection()
    .SetApplicationName("PortfolioDashboard");

// Add Entra ID authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.Name = "PortfolioClientAuth";
    options.Cookie.MaxAge = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
    options.Events.OnValidatePrincipal = async context =>
    {
        Console.WriteLine($"Validating cookie principal: {context.Principal?.Identity?.Name}");
        await Task.CompletedTask;
    };
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://login.microsoftonline.com/917f9213-2bac-4a41-ba47-8026b0e2adea/v2.0";
    options.ClientId = "e5b9d857-5798-4a28-88af-22f9615f1f86";
    options.ClientSecret = builder.Configuration["EntraId:ClientSecret"];
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.CallbackPath = "/signin-oidc";
    options.SignedOutCallbackPath = "/signout-callback-oidc";
    options.RemoteSignOutPath = "/signout-oidc";
    options.StateDataFormat = new PropertiesDataFormat(
        builder.Services.BuildServiceProvider()
            .GetRequiredService<IDataProtectionProvider>()
            .CreateProtector("PortfolioClient.Oidc"));
    options.CorrelationCookie.SameSite = SameSiteMode.Lax;
    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
    options.CorrelationCookie.HttpOnly = true;
    options.CorrelationCookie.Name = "PortfolioClient.Correlation";
    options.NonceCookie.SameSite = SameSiteMode.Lax;
    options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
    options.NonceCookie.HttpOnly = true;
    options.NonceCookie.Name = "PortfolioClient.Nonce";
    options.Events = new OpenIdConnectEvents
    {
        OnAuthenticationFailed = async context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception}, Request Path: {context.HttpContext.Request.Path}");
            await Task.CompletedTask;
        },
        OnTokenValidated = async context =>
        {
            Console.WriteLine($"Token validated successfully, User: {context.Principal?.Identity?.Name}");
            await Task.CompletedTask;
        },
        OnRedirectToIdentityProvider = async context =>
        {
            Console.WriteLine($"Redirecting to identity provider, State: {context.ProtocolMessage.State}, CorrelationId: {context.HttpContext.Request.Cookies[".AspNetCore.Correlation.OpenIdConnect"]}, RedirectUri: {context.ProtocolMessage.RedirectUri}");
            await Task.CompletedTask;
        },
        OnMessageReceived = async context =>
        {
            Console.WriteLine($"Message received, State: {context.ProtocolMessage.State}, CorrelationId: {context.HttpContext.Request.Cookies[".AspNetCore.Correlation.OpenIdConnect"]}, Request Path: {context.HttpContext.Request.Path}");
            await Task.CompletedTask;
        },
        OnRedirectToIdentityProviderForSignOut = async context =>
        {
            Console.WriteLine($"Redirecting to identity provider for sign-out, Request Path: {context.HttpContext.Request.Path}");
            await Task.CompletedTask;
        }
    };
});

// Register HttpClient for API calls
var apiBaseUrl = builder.Configuration["PortfolioApi:BaseUrl"] ?? "https://portfoliodashboardapi-b4bfeje0cwdeenh8.centralus-01.azurewebsites.net/";
builder.Services.AddHttpClient("PortfolioApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Add services
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<GitHubService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();