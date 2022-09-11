using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Commands.CreateGitAccount
{
    public class CreateGitAccountCommandValidator : AbstractValidator<CreateGitAccountCommand>
    {
        public CreateGitAccountCommandValidator()
        {
            RuleFor(c => c.AddressLink).NotEmpty();
        }
    }
}
