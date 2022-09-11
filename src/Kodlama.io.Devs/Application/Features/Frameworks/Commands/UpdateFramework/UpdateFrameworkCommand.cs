using Application.Features.Frameworks.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Frameworks.Commands.UpdateFramework
{
    public class UpdateFrameworkCommand : IRequest<UpdatedFrameworkDto>
    {

        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }




        public class UpdateFrameworkCommandHandler : IRequestHandler<UpdateFrameworkCommand, UpdatedFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;

            public UpdateFrameworkCommandHandler(IFrameworkRepository frameworkRepository, IMapper mapper)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedFrameworkDto> Handle(UpdateFrameworkCommand request, CancellationToken cancellationToken)
            {
                Framework framework = _mapper.Map<Framework>(request);
                Framework updatedFramework = await _frameworkRepository.UpdateAsync(framework);
                UpdatedFrameworkDto updateFrameworkDto = _mapper.Map<UpdatedFrameworkDto>(updatedFramework);

                return updateFrameworkDto;
            }
        }
    }
}
