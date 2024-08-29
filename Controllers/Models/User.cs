namespace Controllers.Models;

public partial class User
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; private set; }

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string password)
    {
        Password = password;
        UpdatedAt = DateTime.UtcNow;
    }
}
