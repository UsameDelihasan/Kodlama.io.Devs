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

namespace Application.Features.GitAccounts.Commands.CreateGitAccount
{
    public class CreateGitAccountCommand : IRequest<CreatedGitAccountDto>
    {
        public int AccountId { get; set; }
        public string AddressLink { get; set; }


        public class CreateGitAccountCommandHandler : IRequestHandler<CreateGitAccountCommand, CreatedGitAccountDto>
        {
            private readonly IGitAccountRepository _gitAccountRepository;
            private readonly IMapper _mapper;

            public CreateGitAccountCommandHandler(IGitAccountRepository gitAccountRepository, IMapper mapper)
            {
                _gitAccountRepository = gitAccountRepository;
                _mapper = mapper;
            }

            public async Task<CreatedGitAccountDto> Handle(CreateGitAccountCommand request, CancellationToken cancellationToken)
            {
                GitAccount framework = _mapper.Map<GitAccount>(request); 

                framework.AccountId = request.AccountId;


                GitAccount addedFramework = await _gitAccountRepository.AddAsync(framework);
                CreatedGitAccountDto createdFrameworkDto = _mapper.Map<CreatedGitAccountDto>(addedFramework);

                return createdFrameworkDto;
            }
        }
    }
}
