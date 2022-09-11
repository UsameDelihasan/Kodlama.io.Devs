using Application.Features.Authorization.Dtos;
using Application.Features.Authorization.Rules;
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
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.UserForRegister
{
    public class UserForRegisterCommand : UserForRegisterDto, IRequest<ResultTokenDto>
    {

        

        public class UserForRegisterCommandHandler : IRequestHandler<UserForRegisterCommand, ResultTokenDto>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationBusinessRules _rules;

            public UserForRegisterCommandHandler(IAccountRepository accountRepository, IMapper mapper, ITokenHelper tokenHelper, AuthorizationBusinessRules rules)
            {
                _accountRepository = accountRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _rules = rules;
            }

            public async Task<ResultTokenDto> Handle(UserForRegisterCommand request, CancellationToken cancellationToken)
            {
                



                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                Account account = _mapper.Map<Account>(request);
                await _rules.CheckIfEmailDuplicated(account.Email);
                

                account.PasswordSalt = passwordSalt;
                account.PasswordHash = passwordHash;

                Account addedAccount = await _accountRepository.AddAsync(account);


                


                var accessToken = _tokenHelper.CreateToken(addedAccount, new List<OperationClaim>());

                ResultTokenDto resultTokenDto = new ResultTokenDto() { Token = accessToken.Token, Expiration = accessToken.Expiration };

                return resultTokenDto;


            }
        }
    }
}
