using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GitAccount :Entity
    {
        public string AddressLink { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public GitAccount()
        {

        }

        public GitAccount(int id, string addressLink, int accountId) : this()
        {
            Id = id;
            AddressLink = addressLink;
            AccountId = accountId;
        }
    }
}
