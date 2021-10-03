using Application.Domain.Core.Bus;
using Application.Domain.Core.Commands;
using Application.Domain.Core.Domain;
using Application.Domain.Core.Queries;
using MediatR;
using System.Threading.Tasks;

namespace Application.Bus
{
    public class MediatorHandler : IEventBus
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<ResponseResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task<ResponseResult> RequestQuery<T>(T queryFilter) where T : QueryFilter
        {
            return _mediator.Send(queryFilter);
        }
    }
}
