using Application.App.Converters.Users;
using Application.App.Queries.Users;
using Application.Data.Repositories;
using Application.Domain.Core.Domain;
using Application.Domain.Core.Validator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.App.QueryHandler
{
    public class ListUserHandler : ValidatorResponse, IRequestHandler<UserQuery, ResponseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResult> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.Table.ToListAsync();

            _response.AddValue(new 
            {
                users = users.UserCollectionToUserDtoCollection()
            });

            return _response;
        }
    }
}
