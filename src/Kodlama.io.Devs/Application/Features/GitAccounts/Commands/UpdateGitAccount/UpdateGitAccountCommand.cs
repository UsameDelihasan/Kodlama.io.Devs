using Application.Features.GitAccounts.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Commands.UpdateGitAccount
{
    public class UpdateGitAccountCommand : IRequest<UpdatedGitAccountDto>
    {

        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }




        public class UpdateGitAccountCommandHandler : IRequestHandler<UpdateGitAccountCommand, UpdatedGitAccountDto>
        {
            private readonly IGitAccountRepository _gitAccountRepository;
            private readonly IMapper _mapper;

            public UpdateGitAccountCommandHandler(IGitAccountRepository gitAccountRepository, IMapper mapper)
            {
                _gitAccountRepository = gitAccountRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedGitAccountDto> Handle(UpdateGitAccountCommand request, CancellationToken cancellationToken)
            {
                GitAccount gitAccount = _mapper.Map<GitAccount>(request);
                GitAccount updatedGitAccount = await _gitAccountRepository.UpdateAsync(gitAccount);
                UpdatedGitAccountDto updatedGitAccountDto = _mapper.Map<UpdatedGitAccountDto>(updatedGitAccount);

                return updatedGitAccountDto;
            }
        }
    }
}
