
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Core.Security.Dtos;
using Application.Features.Authorization.Dtos;
using Application.Features.Authorization.Rules;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authorization.Commands.UserForLogin
{
    public class UserForLoginCommand : UserForLoginDto, IRequest<ResultTokenDto>
    {
        public class UserForLoginCommandHandler : IRequestHandler<UserForLoginCommand, ResultTokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationBusinessRules _rules;

            public UserForLoginCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, AuthorizationBusinessRules rules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _rules = rules;
            }

            public async Task<ResultTokenDto> Handle(UserForLoginCommand request, CancellationToken cancellationToken)
            {

                var userQueried = await _userRepository.GetAsync(
                u => u.Email.ToLower() == request.Email.ToLower(),
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(x => x.OperationClaim));



                List<OperationClaim> operationClaims = new List<OperationClaim>() { };

                foreach (var userOperationClaim in userQueried.UserOperationClaims)
                {
                    operationClaims.Add(userOperationClaim.OperationClaim);
                }

                await _rules.CheckIfUserExist(userQueried);
                await _rules.VerifyOfPasswordHash(request);

                var accessToken = _tokenHelper.CreateToken(userQueried, operationClaims);

                ResultTokenDto resultTokenDto = _mapper.Map<ResultTokenDto>(accessToken);  //map ekle
                return resultTokenDto;

            }
        }
    }
}
