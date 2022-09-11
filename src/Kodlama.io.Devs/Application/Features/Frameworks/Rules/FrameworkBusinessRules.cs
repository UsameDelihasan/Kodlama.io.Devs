using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Frameworks.Rules
{
    public class FrameworkBusinessRules
    {
        private readonly IFrameworkRepository _repository;

        public FrameworkBusinessRules(IFrameworkRepository repository)
        {
            _repository = repository;
        }

        public async Task NameShouldBeTakenOnce(string name)
        {
            IPaginate<Framework> result = await _repository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Framework name already exsist");
        }

        public void FrameworkShouldExist(Framework framework)
        {
            if (framework == null) throw new BusinessException("Framework is not nullable");
        }


    }
}
