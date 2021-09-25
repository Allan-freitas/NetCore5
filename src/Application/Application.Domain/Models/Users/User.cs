using Application.Domain.Core.Domain;

namespace Application.Domain.Models.Users
{
    public class User : Entity
    {
        private User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public static User Create(string username, string email, string password)
        {
            return new User(username, email, password);
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
