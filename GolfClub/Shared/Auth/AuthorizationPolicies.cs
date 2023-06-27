using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
namespace Shared.Auth;

/// <summary>
/// Class that applies Authorization Policies so User can only access certain parts
/// of the page based on their Role
/// </summary>
public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeEmployee", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Employee"));
    
            options.AddPolicy("MustBeMember", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Member"));
        });
    }
}