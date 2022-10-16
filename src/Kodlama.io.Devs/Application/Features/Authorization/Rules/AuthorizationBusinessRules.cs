using Application.Features.Authorization.Commands.UserForLogin;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Rules
{
    public class AuthorizationBusinessRules
    {
        private readonly IAccountRepository _accountRepository;

        public AuthorizationBusinessRules(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //login
        public async Task CheckIfUserExist(User user)
        {
           
            if (user == null) throw new BusinessException("User could not be found!");
        }


        public async Task CheckIfEmailDuplicated(string email)
        {
            var accountExist = await _accountRepository.GetAsync(u => u.Email == email);
            if (accountExist != null) throw new BusinessException("This email is already registered");
        }




        
        public async Task VerifyOfPasswordHash(UserForLoginCommand request)
        {
            var accountExist = await _accountRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
            if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, accountExist.PasswordHash, accountExist.PasswordSalt)) throw new BusinessException("Verification failed! ");
        }

    }
}
