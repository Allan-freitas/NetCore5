using Application.Domain.Core.Commands;
using Application.Domain.Core.Domain;
using Application.Domain.Core.Queries;

namespace Application.Domain.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<ResponseResult> RequestQuery<T>(T queryFilter) where T : QueryFilter;

        Task<ResponseResult> SendCommand<T>(T command) where T : Command;

    }
}
