using Application.Domain.Core.Commands;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Application.App.Commands.Users
{
    public class LoginUserCommand : Command
    {
        [JsonConstructor]
        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<LoginUserCommand>()
                .Requires()
                .IsNotNullOrEmpty(Username, nameof(Username), "O Email não pode ser vazio"));

            AddNotifications(new Contract<LoginUserCommand>()
                .Requires()
                .IsNotNullOrEmpty(Password, nameof(Password), "O Password não pode ser vazio"));
        }
    }
}
