using Application.Domain.Core.Domain;
using Flunt.Notifications;
using MediatR;

namespace Application.Domain.Core.Events
{
    public abstract class Message : Notifiable<Notification>, IRequest<ResponseResult>
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
