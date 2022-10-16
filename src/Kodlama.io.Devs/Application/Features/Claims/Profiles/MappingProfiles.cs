using Application.Features.Claims.Commands.CreateClaim;
using Application.Features.Claims.Commands.UpdateClaim;
using Application.Features.Claims.Dtos;
using Application.Features.Claims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, CreateClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreatedClaimDto>().ReverseMap();

            CreateMap<OperationClaim, DeletedClaimDto>().ReverseMap();

            CreateMap<OperationClaim, UpdateClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdatedClaimDto>().ReverseMap();

            CreateMap<IPaginate<OperationClaim>, ClaimListModel>().ReverseMap();

            CreateMap<OperationClaim, ClaimListDto>().ReverseMap();
            CreateMap<OperationClaim, GetByIdClaimDto> ().ReverseMap();

            

           
        }
    }
}
