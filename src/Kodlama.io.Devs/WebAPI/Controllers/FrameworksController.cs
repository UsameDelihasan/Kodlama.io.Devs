using Application.Features.Frameworks.Commands.CreateFramework;
using Application.Features.Frameworks.Commands.DeleteFramework;
using Application.Features.Frameworks.Commands.UpdateFramework;
using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Models;
using Application.Features.Frameworks.Queries.GetByIdFramework;
using Application.Features.Frameworks.Queries.GetByIdFrameworkQuery;
using Application.Features.Frameworks.Queries.GetListFrameworkByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrameworksController : BaseController
    {

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdFrameworkQuery query)
        {
            GetByIdFrameworkDto GetByIdDto = await Mediator.Send(query);

            return Ok(GetByIdDto);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFrameworkQuery getListFrameworkQuery = new GetListFrameworkQuery() { PageRequest = pageRequest };
            FrameworkListModel result = await Mediator.Send(getListFrameworkQuery);
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]

        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListFrameworkByDynamicQuery getListFrameworkByDynamicQuery = new GetListFrameworkByDynamicQuery() { PageRequest = pageRequest, Dynamic = dynamic };
            FrameworkListModel result = await Mediator.Send(getListFrameworkByDynamicQuery);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateFrameworkCommand command)
        {
            CreatedFrameworkDto dto = await Mediator.Send(command);

            return Created("", dto);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteFrameworkCommand command)
        {

            DeletedFrameworkDto deletedFrameworkDto = await Mediator.Send(command);

            return Ok(deletedFrameworkDto);

        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateFrameworkCommand command)
        {
            UpdatedFrameworkDto updatedFrameworkDto = await Mediator.Send(command);

            return Ok(updatedFrameworkDto);
        }



    }
}
