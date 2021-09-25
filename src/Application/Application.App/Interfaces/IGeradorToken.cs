using Application.App.Dtos.Token;
using Application.Domain.Models.Users;

namespace Application.App.Interfaces
{
    public interface IGeradorToken
    {
        JsonWebTokenDto GerarToken(User user);

        RefreshTokenDto GerarRefreshToken(User user);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
