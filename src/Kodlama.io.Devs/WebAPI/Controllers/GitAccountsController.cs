using Application.Features.GitAccounts.Commands.CreateGitAccount;
using Application.Features.GitAccounts.Commands.DeleteGitAccount;
using Application.Features.GitAccounts.Commands.UpdateGitAccount;
using Application.Features.GitAccounts.Dtos;
using Application.Features.GitAccounts.Models;
using Application.Features.GitAccounts.Queries.GetListGitAccount;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitAccountsController : BaseController
    {
        

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGitAccountQuery getListGitAccountQuery = new GetListGitAccountQuery() { PageRequest = pageRequest };
            GitAccountListModel result = await Mediator.Send(getListGitAccountQuery);
            return Ok(result);
        }

        

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateGitAccountCommand command)
        {
            CreatedGitAccountDto dto = await Mediator.Send(command);

            return Created("", dto);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGitAccountCommand command)
        {

            DeletedGitAccountDto deletedGitAccoundDto = await Mediator.Send(command);

            return Ok(deletedGitAccoundDto);

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateGitAccountCommand command)
        {
            UpdatedGitAccountDto updatedGitAccoundDto = await Mediator.Send(command);

            return Ok(updatedGitAccoundDto);
        }

    }
}
