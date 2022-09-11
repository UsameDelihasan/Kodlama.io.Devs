using Application.Features.Authorization.Commands.UserForRegister;
using Application.Features.Authorization.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserForRegisterCommand, Account>().ReverseMap();
            CreateMap<AccessToken, ResultTokenDto>().ReverseMap();

        }
    }
}
