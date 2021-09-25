using Application.Domain.Core.Domain;
using Flunt.Notifications;
using MediatR;

namespace Application.Domain.Core.Queries
{
    public class QueryFilter : Notifiable<Notification>, IRequest<ResponseResult>
    {
    }
}
