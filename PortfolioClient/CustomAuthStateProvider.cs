using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace PortfolioClient;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private AuthenticationState _cachedAuthState;

    public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
        _cachedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return _cachedAuthState;
    }

    public async Task UpdateAuthenticationStateAsync()
    {
        var result = await _sessionStorage.GetAsync<string>("access_token");
        Console.WriteLine($"Access token found: {result.Success}, Value: {(result.Success ? result.Value : "None")}");
        if (result.Success && !string.IsNullOrEmpty(result.Value))
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "User"), // Replace with actual Entra ID claims
                new Claim("access_token", result.Value)
            };
            var identity = new ClaimsIdentity(claims, "EntraID");
            var user = new ClaimsPrincipal(identity);
            _cachedAuthState = new AuthenticationState(user);
        }
        else
        {
            _cachedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        NotifyAuthenticationStateChanged(Task.FromResult(_cachedAuthState));
    }

    public async Task ClearAuthenticationStateAsync()
    {
        await _sessionStorage.DeleteAsync("access_token");
        _cachedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedAuthState));
    }
}