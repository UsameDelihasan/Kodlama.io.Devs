using Application.Features.Authorization.Dtos;
using Application.Features.Authorization.Rules;
using Application.Services.AuthServices;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.UserForRegister
{
    public class UserForRegisterCommand :  IRequest<RegisteredDto>
    {

        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }


        public class UserForRegisterCommandHandler : IRequestHandler<UserForRegisterCommand, RegisteredDto>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationBusinessRules _rules;
            private readonly IAuthService _authService;

            public UserForRegisterCommandHandler(IAccountRepository accountRepository, IMapper mapper, ITokenHelper tokenHelper, AuthorizationBusinessRules rules, IAuthService authService)
            {
                _accountRepository = accountRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _rules = rules;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(UserForRegisterCommand request, CancellationToken cancellationToken)
            {

                await _rules.CheckIfEmailDuplicated(request.UserForRegisterDto.Email);


                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                //Account account = _mapper.Map<Account>(request);

                Account newAccount = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true

                };



                newAccount.PasswordSalt = passwordSalt;
                newAccount.PasswordHash = passwordHash;

                Account createdAccount = await _accountRepository.AddAsync(newAccount);


                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdAccount);

                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdAccount, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,
                };


                return registeredDto;
            }
        }
    }
}
