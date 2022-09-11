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
            RuleFor(r => r.FirstName).NotEmpty().NotNull();
            RuleFor(r => r.LastName).NotEmpty().NotNull(); ; 
            RuleFor(r => r.Email).NotEmpty().NotNull(); ;
            RuleFor(r => r.Password).NotEmpty().NotNull(); ;   
        }
    }
}
