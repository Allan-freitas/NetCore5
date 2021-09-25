using Application.Domain.Core.Domain;
using Application.Domain.Core.Queries;
using MediatR;

namespace Application.App.Queries.Users
{
    public class UserQuery : QueryFilter, IRequest<ResponseResult> { }
}
