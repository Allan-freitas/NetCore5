namespace Application.App.Dtos.Users
{
    public class UserDto
    {
        public UserDto(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
