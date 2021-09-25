namespace Application.App.Dtos.Token
{
    public class JsonWebTokenDto
    {
        public string? Token { get; set; }

        public RefreshTokenDto? RefreshToken { get; set; }

        public string TipoToken { get; } = "bearer";

        public long ExpiraEmSegundos { get; set; }
    }
}
