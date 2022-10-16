using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
using Application.Features.UserOperationClaims.Commands.DeleteUserClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimController : BaseController
    {

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest request)
        {
            GetListUserOperationClaimQuery query = new() { PageRequest = request };
            UserOperationClaimListModel model = await Mediator.Send(query);

            return Ok(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand command)
        {
            CreatedUserOperationClaimDto dto = await Mediator.Send(command);

            return Created("", dto);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand command)
        {

            DeletedUserOperationClaimDto deletedUserOperationClaimDto = await Mediator.Send(command);

            return Ok(deletedUserOperationClaimDto);

        }

    }
}
