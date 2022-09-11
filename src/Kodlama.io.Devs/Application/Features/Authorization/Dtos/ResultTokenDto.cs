using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Dtos
{
    public class ResultTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
