using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Frameworks.Queries.GetByIdFramework
{
    public class GetByIdFrameworkQuery : IRequest<GetByIdFrameworkDto>
    {
        public int Id { get; set; }

        public class GetByIdFrameworkQueryHandler : IRequestHandler<GetByIdFrameworkQuery, GetByIdFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;
            private readonly FrameworkBusinessRules _frameworkBusinessRules;

            public GetByIdFrameworkQueryHandler(IFrameworkRepository frameworkRepository, IMapper mapper, FrameworkBusinessRules frameworkBusinessRules)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
                _frameworkBusinessRules = frameworkBusinessRules;
            }

            public async Task<GetByIdFrameworkDto> Handle(GetByIdFrameworkQuery request, CancellationToken cancellationToken)
            {

                Framework framework = await _frameworkRepository.GetAsync(f=>f.Id == request.Id);
                 _frameworkBusinessRules.FrameworkShouldExist(framework);

                GetByIdFrameworkDto getByIdFrameworkDto = _mapper.Map<GetByIdFrameworkDto>(framework);

                return getByIdFrameworkDto;
            }
        }
    }
}
