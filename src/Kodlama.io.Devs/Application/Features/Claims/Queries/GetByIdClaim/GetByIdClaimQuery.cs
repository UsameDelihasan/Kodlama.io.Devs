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

namespace Application.Features.Claims.Queries.GetByIdClaim
{
    public class GetByIdClaimQuery : IRequest<GetByIdClaimDto>
    {
            public int Id { get; set; }

            public class GetByIdClaimQueryHandler : IRequestHandler<GetByIdClaimQuery, GetByIdClaimDto>
            {
                private readonly IOperationClaimRepository _operationClaimRepository;
                private readonly IMapper _mapper;
                private readonly ClaimBusinessRules _claimBusinessRules;

                public GetByIdClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ClaimBusinessRules claimBusinessRules)
                {
                    _operationClaimRepository = operationClaimRepository;
                    _mapper = mapper;
                    _claimBusinessRules = claimBusinessRules;
                }
            public async Task<GetByIdClaimDto> Handle(GetByIdClaimQuery request, CancellationToken cancellationToken)
                {


                    OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == request.Id);

                //_claimBusinessRules.ClaimShouldExist();

                GetByIdClaimDto getByIdClaimDto = _mapper.Map<GetByIdClaimDto>(operationClaim);

                    return getByIdClaimDto;

                }
            }
        }
    
}
