using Application.Domain.Core.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection;

namespace Application.WebApi.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IEventBus _eventBus;
        protected IEventBus EventBus => _eventBus ??= HttpContext.RequestServices.GetService<IEventBus>();

    }
}
