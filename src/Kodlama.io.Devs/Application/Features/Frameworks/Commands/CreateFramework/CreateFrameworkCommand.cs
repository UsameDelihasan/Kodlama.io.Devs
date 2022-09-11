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

namespace Application.Features.Frameworks.Commands.CreateFramework
{
    public class CreateFrameworkCommand : IRequest<CreatedFrameworkDto>
    {
        
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }




        public class CreateFrameworkCommandHandler : IRequestHandler<CreateFrameworkCommand, CreatedFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;
            private readonly FrameworkBusinessRules frameworkBusinessRules;

            public CreateFrameworkCommandHandler(IFrameworkRepository frameworkRepository, IMapper mapper, FrameworkBusinessRules frameworkBusinessRules)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
                this.frameworkBusinessRules = frameworkBusinessRules;
            }

            public async Task<CreatedFrameworkDto> Handle(CreateFrameworkCommand request, CancellationToken cancellationToken)
            {
                await frameworkBusinessRules.NameShouldBeTakenOnce(request.Name);

                

                Framework framework = _mapper.Map<Framework>(request);

                Framework addedFramework = await _frameworkRepository.AddAsync(framework);
                CreatedFrameworkDto createdFrameworkDto = _mapper.Map<CreatedFrameworkDto>(addedFramework);

                return createdFrameworkDto;
            }
        }
    }
}
