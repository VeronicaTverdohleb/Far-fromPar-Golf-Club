namespace Shared.Dtos;
/// <summary>
/// Data Transfer Object used in UserLogin-related use cases
/// </summary>
public class UserLoginDto
{
    public string Username { get; init; }
    public string Password { get; init; }
}