using Application.Features.GitAccounts.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitAccounts.Models
{
    public class GitAccountListModel : BasePageableModel
    {
        public IList<GitAccountListDto> Items { get; set; }
    }
}
