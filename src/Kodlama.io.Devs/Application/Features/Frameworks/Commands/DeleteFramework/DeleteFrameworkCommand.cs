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

namespace Application.Features.Frameworks.Commands.DeleteFramework
{
    public class DeleteFrameworkCommand :IRequest<DeletedFrameworkDto>
    {
        public int Id { get; set; }

        public class DeleteFrameworkCommandHandler : IRequestHandler<DeleteFrameworkCommand, DeletedFrameworkDto>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;

            public DeleteFrameworkCommandHandler(IFrameworkRepository frameworkRepository, IMapper mapper)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
            }

            public async Task<DeletedFrameworkDto> Handle(DeleteFrameworkCommand request, CancellationToken cancellationToken)
            {
                Framework framework = await _frameworkRepository.GetAsync(f => f.Id == request.Id);
                Framework deletedFramework = await _frameworkRepository.DeleteAsync(framework);
                DeletedFrameworkDto deletedFrameworkDto = _mapper.Map<DeletedFrameworkDto>(deletedFramework);

                return deletedFrameworkDto;
            }
        }
    }
}
