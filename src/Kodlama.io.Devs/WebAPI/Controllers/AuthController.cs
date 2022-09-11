using Application.Features.Authorization.Commands.UserForLogin;
using Application.Features.Authorization.Commands.UserForRegister;
using Application.Features.Authorization.Dtos;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserForRegisterCommand userForRegisterCommand)
        {
            ResultTokenDto resultTokenDto = await Mediator.Send(userForRegisterCommand);

            return Ok(resultTokenDto);
        }






        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginCommand userForLoginCommand)
        {
            ResultTokenDto resultTokenDto = await Mediator.Send(userForLoginCommand);

            return Ok(resultTokenDto);
        }
    }
}
