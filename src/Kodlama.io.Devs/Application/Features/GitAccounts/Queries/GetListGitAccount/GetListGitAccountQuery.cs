using Application.Features.GitAccounts.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Queries.GetListGitAccount
{
    public class GetListGitAccountQuery : IRequest<GitAccountListModel>
    {
        public PageRequest PageRequest { get; set; }

       

        public class GetListGitAccountQueryHandler : IRequestHandler<GetListGitAccountQuery, GitAccountListModel>
        {
            private readonly IGitAccountRepository _gitAccountRepository;
            private readonly IMapper _mapper;

            public GetListGitAccountQueryHandler(IGitAccountRepository gitAccountRepository, IMapper mapper)
            {
                _gitAccountRepository = gitAccountRepository;
                _mapper = mapper;
            }

            public async Task<GitAccountListModel> Handle(GetListGitAccountQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GitAccount> gitAccounts = await _gitAccountRepository.GetListAsync(include:
                                                                                    f => f.Include(c => c.Account),
                                                                                    index: request.PageRequest.Page,
                                                                                    size: request.PageRequest.PageSize
                                                                                    );

                GitAccountListModel mappedModels = _mapper.Map<GitAccountListModel>(gitAccounts);
                return mappedModels;

            }
        }
    }
}
