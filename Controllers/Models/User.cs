namespace Controllers.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}