
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
using Application.Services.AuthServices;

namespace Application.Features.Authorization.Commands.UserForLogin
{
    public class UserForLoginCommand :  IRequest<LoginDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string ipAdress { get; set; }

        public class UserForLoginCommandHandler : IRequestHandler<UserForLoginCommand, LoginDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthorizationBusinessRules _rules;
            private readonly IAuthService _authService;

            public UserForLoginCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, AuthorizationBusinessRules rules, IAuthService authService)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _rules = rules;
                _authService = authService;
            }

            public async Task<LoginDto> Handle(UserForLoginCommand request, CancellationToken cancellationToken)
            {

                var userQueried = await _userRepository.GetAsync(
                u => u.Email.ToLower() == request.UserForLoginDto.Email.ToLower(),
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(x => x.OperationClaim));

                

                List<OperationClaim> operationClaims = new List<OperationClaim>() { };

                foreach (var userOperationClaim in userQueried.UserOperationClaims)
                {
                    operationClaims.Add(userOperationClaim.OperationClaim);
                }

                await _rules.CheckIfUserExist(userQueried);

                await _rules.VerifyOfPasswordHash(request);

                AccessToken accessToken = await _authService.CreateAccessToken(userQueried);
                RefreshToken createRefreshedToken = await _authService.CreateRefreshToken(userQueried, request.ipAdress);
                RefreshToken refreshedToken = await _authService.AddRefreshToken(createRefreshedToken);

                LoginDto loginDto = new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshedToken
                };

                return loginDto;

            }
        }
    }
}
