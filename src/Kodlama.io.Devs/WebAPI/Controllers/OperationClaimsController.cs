using Application.Features.Claims.Commands.CreateClaim;
using Application.Features.Claims.Commands.DeleteClaim;
using Application.Features.Claims.Commands.UpdateClaim;
using Application.Features.Claims.Dtos;
using Application.Features.Claims.Models;
using Application.Features.Claims.Queries.GetByIdClaim;
using Application.Features.Claims.Queries.GetListClaim;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest request)
        {
            GetListClaimQuery query = new() { PageRequest = request };
            ClaimListModel model = await Mediator.Send(query);

            return Ok(model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdClaimQuery query)
        {
            GetByIdClaimDto GetByIdDto = await Mediator.Send(query);

            return Ok(GetByIdDto);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateClaimCommand command)
        {
            CreatedClaimDto dto = await Mediator.Send(command);

            return Created("", dto);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteClaimCommand command)
        {

            DeletedClaimDto deletedClaimDto = await Mediator.Send(command);

            return Ok(deletedClaimDto);

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateClaimCommand command)
        {
            UpdatedClaimDto updatedClaimDto = await Mediator.Send(command);

            return Ok(updatedClaimDto);
        }
    }
}
