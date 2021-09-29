using Application.App.Commands.Users;
using Application.Domain.Core.Domain;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Notification>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseResult>> Login([FromBody] LoginUserCommand command)
        {
            var response = await EventBus.SendCommand(command);
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok(response.Data);
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IReadOnlyCollection<Notification>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] RegisterUserCommand command)
        {
            var response = await EventBus.SendCommand(command);
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok();
        }
    }
}
