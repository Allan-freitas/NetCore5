using Application.Domain.Core.Commands;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Application.App.Commands.Users
{
    public class RegisterUserCommand : Command
    {
        [JsonConstructor]
        public RegisterUserCommand(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<RegisterUserCommand>()
                .Requires()
                .IsNotNullOrEmpty(Username, nameof(Username), "O Username não pode ser vazio"));

            AddNotifications(new Contract<RegisterUserCommand>()
                .Requires()
                .IsNotNullOrEmpty(Password, nameof(Password), "O Password não pode ser vazio"));


            AddNotifications(new Contract<RegisterUserCommand>()
                .Requires()
                .IsNotNullOrEmpty(Email, nameof(Email), "O E-mail não pode ser vazio"));


            AddNotifications(new Contract<RegisterUserCommand>()
                .Requires()
                .IsEmail(Email, nameof(Email), "O E-mail deve ser um e-mail válido"));
        }
    }
}
