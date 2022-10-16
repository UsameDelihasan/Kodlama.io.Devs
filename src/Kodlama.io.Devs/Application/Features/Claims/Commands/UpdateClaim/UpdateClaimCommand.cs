using Application.Features.Claims.Dtos;
using Application.Features.Claims.Rules;
using Application.Features.Frameworks.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommand : IRequest<UpdatedClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, UpdatedClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ClaimBusinessRules _claimBusinessRules;

            public UpdateClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ClaimBusinessRules claimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<UpdatedClaimDto> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim operationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
                UpdatedClaimDto updateOperationClaimDto = _mapper.Map<UpdatedClaimDto>(updatedOperationClaim);

                return updateOperationClaimDto;
            }
        }
    }
}
