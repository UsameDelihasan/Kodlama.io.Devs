using Application.Features.Authorization.Commands.UserForLogin;
using Application.Features.Authorization.Commands.UserForRegister;
using Application.Features.Authorization.Dtos;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        
        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            UserForRegisterCommand userForRegisterCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAdress()
            };


            RegisteredDto registeredDto = await Mediator.Send(userForRegisterCommand);

            SetRefreshTokenToCookie(registeredDto.RefreshToken);



            return Created("", registeredDto.AccessToken);
        }






        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            UserForLoginCommand userForLoginCommand = new()
            {
                UserForLoginDto = userForLoginDto,
                ipAdress = GetIpAdress()
            };
            LoginDto loginDto = await Mediator.Send(userForLoginCommand);

            SetRefreshTokenToCookie(loginDto.RefreshToken);



            return Created("",loginDto.AccessToken);
        }


        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() {  Expires = DateTime.Now.AddDays(7), HttpOnly=true };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }

    }




}
