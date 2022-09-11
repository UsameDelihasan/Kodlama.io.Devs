using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Dtos
{
    public class CreatedGitAccountDto
    {
        public int Id { get; set; }
        public string AddressLink { get; set; }
        public int AccountId { get; set; }
    }
}
