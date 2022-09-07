using Application.Features.ProgrammingLanguages.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }


        

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgrammingLanguageRepository _repository;
            
            public DeleteProgrammingLanguageCommandHandler(IMapper mapper, IProgrammingLanguageRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage programmingLanguage =await  _repository.GetAsync(p =>p.Id == request.Id);
                ProgrammingLanguage deletedProgrammingLanguage = await _repository.DeleteAsync(programmingLanguage);
                DeletedProgrammingLanguageDto result = _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

                return result;
            }
        }
    }
}
