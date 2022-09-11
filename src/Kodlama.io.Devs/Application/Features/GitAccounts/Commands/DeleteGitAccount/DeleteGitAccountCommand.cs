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

namespace Application.Features.GitAccounts.Commands.DeleteGitAccount
{
    public class DeleteGitAccountCommand : IRequest<DeletedGitAccountDto>
    {
        public int Id { get; set; }

        public class DeleteGitAccountCommandHandler : IRequestHandler<DeleteGitAccountCommand, DeletedGitAccountDto>
        {
            private readonly IGitAccountRepository _gitAccountRepository;
            private readonly IMapper _mapper;

            public DeleteGitAccountCommandHandler(IGitAccountRepository gitAccountRepository, IMapper mapper)
            {
                _gitAccountRepository = gitAccountRepository;
                _mapper = mapper;
            }

            public async Task<DeletedGitAccountDto> Handle(DeleteGitAccountCommand request, CancellationToken cancellationToken)
            {
                GitAccount gitAccount = await _gitAccountRepository.GetAsync(f => f.Id == request.Id);
                GitAccount deletedGitAccount = await _gitAccountRepository.DeleteAsync(gitAccount);
                DeletedGitAccountDto deletedGitDto = _mapper.Map<DeletedGitAccountDto>(deletedGitAccount);

                return deletedGitDto;
            }
        }
    }
}
