using Application.Features.Claims.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Models
{
    public class ClaimListModel : BasePageableModel
    {
        public IList<ClaimListDto> Items { get; set; }
    }
}
