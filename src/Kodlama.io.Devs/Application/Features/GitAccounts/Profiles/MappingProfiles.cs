using Application.Features.GitAccounts.Commands.CreateGitAccount;
using Application.Features.GitAccounts.Commands.UpdateGitAccount;
using Application.Features.GitAccounts.Dtos;
using Application.Features.GitAccounts.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitAccount, CreateGitAccountCommand>().ReverseMap();
            CreateMap<GitAccount, CreatedGitAccountDto>().ReverseMap();

            CreateMap<GitAccount, DeletedGitAccountDto>().ReverseMap();

            CreateMap<GitAccount, UpdateGitAccountCommand>().ReverseMap();
            CreateMap<GitAccount, UpdatedGitAccountDto>().ReverseMap();


            CreateMap<IPaginate<GitAccount>, GitAccountListModel>().ReverseMap();


            CreateMap<GitAccount, GitAccountListDto>()
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.Account.FirstName + ' '+ c.Account.LastName))
                .ReverseMap();





        }
    }
}
