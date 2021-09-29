using Application.Domain.Core.Commands;
using Application.Domain.Core.Domain;
using Application.Domain.Core.Queries;
using System.Threading.Tasks;

namespace Application.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task<ResponseResult> RequestQuery<T>(T query) where T : QueryFilter;

        Task<ResponseResult> SendCommand<T>(T command) where T : Command;
    }
}
