using Application.App.Commands.Users;
using Application.Data.Repositories;
using Application.Domain.Core.Domain;
using Application.Domain.Core.Validator;
using Application.Domain.Models.Users;
using Flunt.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.App.CommandHandler
{
    public class RegisterUserHandler : ValidatorResponse, IRequestHandler<RegisterUserCommand, ResponseResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (request.Notifications.Any())
            {
                _response.AddNotifications(request.Notifications);
                return _response;
            }

            var user = await _unitOfWork.UserRepository.Table.Where(u => u.Email == request.Username).FirstOrDefaultAsync();

            if (user != null)
            {
                _response.AddNotification(new Notification("usuário", "Usuário já existe"));
                return _response;
            }

            //hash password
            var passwordHash = _passwordHasher.HashPassword(user, request.Password);

            await _unitOfWork.UserRepository.InsertAsync(User
                .Create(request.Username, request.Email, passwordHash));

            await _unitOfWork.CommitAsync();

            return _response;
        }
    }
}
