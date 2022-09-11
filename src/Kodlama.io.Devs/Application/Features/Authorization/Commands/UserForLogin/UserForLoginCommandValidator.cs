using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.UserForLogin
{
    public class UserForLoginCommandValidator:AbstractValidator<UserForLoginCommand>
    {
        public UserForLoginCommandValidator()
        {

            RuleFor(r => r.Email).NotEmpty().NotNull();
            RuleFor(r => r.Password).NotEmpty().NotNull();
        }
    }
}
