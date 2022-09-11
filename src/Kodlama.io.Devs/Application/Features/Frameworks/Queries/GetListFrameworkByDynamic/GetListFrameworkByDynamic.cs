using Application.Features.Frameworks.Models;
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

namespace Application.Features.Frameworks.Queries.GetListFrameworkByDynamic
{

    public class GetListFrameworkByDynamicQuery : IRequest<FrameworkListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListFrameworkByDynamicQueryHandler : IRequestHandler<GetListFrameworkByDynamicQuery, FrameworkListModel>
        {
            private readonly IFrameworkRepository _frameworkRepository;
            private readonly IMapper _mapper;

            public GetListFrameworkByDynamicQueryHandler(IFrameworkRepository frameworkRepository, IMapper mapper)
            {
                _frameworkRepository = frameworkRepository;
                _mapper = mapper;
            }

            public async Task<FrameworkListModel> Handle(GetListFrameworkByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Framework> frameworks = await _frameworkRepository.GetListByDynamicAsync(request.Dynamic,
                                                                                                    include:
                                                                                                    f =>f.Include   (c=>c.ProgrammingLanguage),
                                                                                                    index:request.PageRequest.Page,
                                                                                                    size: request.PageRequest.PageSize
                                                                                                    );


                FrameworkListModel mappedModels= _mapper.Map<FrameworkListModel>(frameworks);

                return mappedModels;
            }
        }
    }
   
}
