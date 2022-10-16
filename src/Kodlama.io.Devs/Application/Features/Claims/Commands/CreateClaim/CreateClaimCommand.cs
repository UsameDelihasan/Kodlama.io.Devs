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

namespace Application.Features.Claims.Commands.CreateClaim
{
    public class CreateClaimCommand : IRequest<CreatedClaimDto>
    {
        public string Name { get; set; }


        public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, CreatedClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ClaimBusinessRules  _claimBusinessRules;

            public CreateClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ClaimBusinessRules claimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<CreatedClaimDto> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
            {
                //await _claimBusinessRules.ClaimExist(request.Name);



                OperationClaim  operationClaim = _mapper.Map<OperationClaim>(request);

                OperationClaim addedOperationClaim = await _operationClaimRepository.AddAsync(operationClaim);
                CreatedClaimDto createdOperationClaimDto = _mapper.Map<CreatedClaimDto>(addedOperationClaim);

                return createdOperationClaimDto;
            }
        }

    }
}
