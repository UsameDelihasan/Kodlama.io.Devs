using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Dtos
{
    public class DeletedUserOperationClaimDto
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}
