using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.UserForRegister
{
    public class UserForRegisterCommandValidator : AbstractValidator<UserForRegisterCommand>
    {
        public UserForRegisterCommandValidator()
        {
            RuleFor(r => r.UserForRegisterDto.FirstName).NotEmpty().NotNull();
            RuleFor(r => r.UserForRegisterDto.LastName).NotEmpty().NotNull(); ; 
            RuleFor(r => r.UserForRegisterDto.Email).NotEmpty().NotNull(); ;
            RuleFor(r => r.UserForRegisterDto.Password).NotEmpty().NotNull(); ;   
        }
    }
}
