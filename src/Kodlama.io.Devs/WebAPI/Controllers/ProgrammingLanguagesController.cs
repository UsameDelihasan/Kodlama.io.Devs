using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand command)
        {
            CreatedProgrammingLanguageDto dto = await Mediator.Send(command);

            return Created("", dto);
        }

        [HttpGet]
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
