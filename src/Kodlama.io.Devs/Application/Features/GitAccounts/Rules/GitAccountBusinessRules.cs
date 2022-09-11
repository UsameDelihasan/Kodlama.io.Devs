using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Rules
{
    public class GitAccountBusinessRules
    {
        private readonly IGitAccountRepository _repository;

        public GitAccountBusinessRules(IGitAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task NameShouldBeTakenOnce(string addressLink)
        {
            IPaginate<GitAccount> result = await _repository.GetListAsync(p => p.AddressLink == addressLink);
            if (result.Items.Any()) throw new BusinessException("Link is  already exsist");
        }

        public void GitAccountShouldExist(GitAccount gitAccount)
        {
            if (gitAccount == null) throw new BusinessException("GitAccount is not nullable");
        }


    }
}
