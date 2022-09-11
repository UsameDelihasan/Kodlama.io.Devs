using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguageByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {

        

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest request)
        {
            GetListProgrammingLanguageQuery query = new() { PageRequest = request };
            ProgrammingLanguageListModel model = await Mediator.Send(query);

            return Ok(model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery query)
        {
            GetByIdProgrammingLanguageDto GetByIdDto = await Mediator.Send(query);

            return Ok(GetByIdDto);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListProgrammingLanguageByDynamicQuery query = new GetListProgrammingLanguageByDynamicQuery() { PageRequest =pageRequest, Dynamic = dynamic };
            ProgrammingLanguageListModel result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand command)
        {
            CreatedProgrammingLanguageDto dto = await Mediator.Send(command);

            return Created("", dto);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProgrammingLanguageCommand command)
        {
            
            DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = await Mediator.Send(command);

            return Ok(deletedProgrammingLanguageDto);

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand command)
        {
            UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = await Mediator.Send(command);

            return Ok(updatedProgrammingLanguageDto);
        }
    }
}
