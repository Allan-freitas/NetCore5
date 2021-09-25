using Application.App.Commands.Users;
using Application.App.Interfaces;
using Application.Data.Repositories;
using Application.Domain.Core.Domain;
using Application.Domain.Core.Validator;
using Application.Domain.Models.Users;
using Flunt.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.App.CommandHandler
{
    public class LoginUserHandler : ValidatorResponse, IRequestHandler<LoginUserCommand, ResponseResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeradorToken _geradorToken;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginUserHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, IGeradorToken geradorToken)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _geradorToken = geradorToken;
        }

        public async Task<ResponseResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Notifications.Any())
            {
                _response.AddNotifications(request.Notifications);
                return _response;
            }

            var usuario = await _unitOfWork.UserRepository.Table.Where(u => u.Email == request.Username).FirstOrDefaultAsync();
            if (usuario == null)
            {
                _response.AddNotification(new Notification("usuario", "Usuário ou senha inválidos"));
                return _response;
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, request.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                _response.AddNotification(new Notification("usuario", "Usuário ou senha inválidos"));
                return _response;
            }

            var jwt = _geradorToken.GerarToken(usuario);
            _response.AddValue(new
            {
                email = usuario.Email,
                jwt
            });

            return _response;
        }
    }
}
