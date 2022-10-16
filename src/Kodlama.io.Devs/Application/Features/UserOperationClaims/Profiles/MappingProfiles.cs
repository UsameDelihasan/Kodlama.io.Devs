using Application.Features.Claims.Commands.CreateClaim;
using Application.Features.Claims.Commands.UpdateClaim;
using Application.Features.Claims.Dtos;
using Application.Features.Claims.Models;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();

            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();

            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();


            CreateMap<UserOperationClaim, UserOperationClaimListDto>()
                .ForMember(c => c.FirstName, opt => opt.MapFrom(c => c.User.FirstName))
                .ReverseMap();





        }
    }
}
