using Application.Features.Claims.Dtos;
using Application.Features.Claims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Commands.DeleteClaim
{
    public class DeleteClaimCommand : IRequest<DeletedClaimDto>
    {
        public int Id { get; set; }

        public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand, DeletedClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ClaimBusinessRules _claimBusinessRules;

            public DeleteClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ClaimBusinessRules claimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<DeletedClaimDto> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = await _operationClaimRepository.GetAsync(f => f.Id == request.Id);
                OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);
                DeletedClaimDto deletedOperationClaimDto = _mapper.Map<DeletedClaimDto>(deletedOperationClaim);

                return deletedOperationClaimDto;
            }
        }
    }
}
