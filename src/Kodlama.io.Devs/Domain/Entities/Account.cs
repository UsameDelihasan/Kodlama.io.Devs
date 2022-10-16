using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account :User
    {
        public virtual ICollection<GitAccount> GitAccounts { get; set; }

        
        

        public Account()
        {

        }

        public Account(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status, AuthenticatorType authenticatorType, int gitHubProfileId):base(id,firstName,lastName,email,passwordSalt,passwordHash,status, authenticatorType)
        {

        }
    }
}
