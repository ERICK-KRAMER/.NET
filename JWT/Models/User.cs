namespace JWT.Models
{
    public record User(
        Guid Id,
        string Email,
        string Password,
        string[] Roles
    );
}