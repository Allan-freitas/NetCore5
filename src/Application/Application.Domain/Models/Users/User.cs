using Application.Domain.Core.Domain;

namespace Application.Domain.Models.Users
{
    public class User : Entity
    {
        private User(long userId, string username, string email, string password)
        {
            Id = userId;
            Username = username;
            Email = email;
            Password = password;
        }

        private User(string username, string email, string password)
        {            
            Username = username;
            Email = email;
            Password = password;
        }

        public static User Create(long userId, string username, string email, string password)
        {
            return new User(userId, username, email, password);
        }

        public static User Create(string username, string email, string password)
        {
            return new User(username, email, password);
        }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}
