using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguageByDynamic
{
    public class GetListProgrammingLanguageByDynamicQuery : IRequest<ProgrammingLanguageListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListProgrammingLanguageByDynamicQueryHandler : IRequestHandler<GetListProgrammingLanguageByDynamicQuery, ProgrammingLanguageListModel>
        {
            private readonly IProgrammingLanguageRepository _repository;
            private readonly IMapper _mapper;

            public GetListProgrammingLanguageByDynamicQueryHandler(IProgrammingLanguageRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguage> programmingLanguages = await _repository.GetListByDynamicAsync(request.Dynamic,
                                                                                                    index: request.PageRequest.Page,
                                                                                                    size: request.PageRequest.PageSize
                                                                                                    );
                ProgrammingLanguageListModel mappedProgrammingLanguageListModel = _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages);
                return mappedProgrammingLanguageListModel;
            }
        }
    }
}
