using Application.Domain.Core.Domain;

namespace Application.Domain.Models.Users
{
    public class User : Entity
    {
        private User() { }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public class UserBuilder
        {
            private readonly User _user = new();

            public UserBuilder AddId(long id)
            {
                _user.Id = id;
                return this;
            }

            public UserBuilder AddUsername(string username)
            {
                _user.Username = username;
               
                return this;
            }
            public UserBuilder AddEmail(string email)
            {
                _user.Email = email;
                return this;
            }

            public UserBuilder AddPassword(string password)
            {
                _user.Password = password;
                return this;
            }

            public User Build()
            {
                return _user;
            }
        }
    }
}
