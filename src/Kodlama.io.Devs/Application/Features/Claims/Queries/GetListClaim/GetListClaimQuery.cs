using Application.Features.Claims.Models;
using Application.Features.Claims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Queries.GetListClaim
{
    public class GetListClaimQuery:IRequest<ClaimListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListClaimQueryHandler : IRequestHandler<GetListClaimQuery, ClaimListModel>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ClaimBusinessRules _claimBusinessRules;

            public GetListClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ClaimBusinessRules claimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<ClaimListModel> Handle(GetListClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                ClaimListModel mappedOperationClaimListModel = _mapper.Map<ClaimListModel>(operationClaims);
                return mappedOperationClaimListModel;

            }
        }
    }
}
