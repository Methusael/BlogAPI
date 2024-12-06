using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBackend.Application.Features.Login.Responses
{
    [Serializable]
    public class LoginResponse
    {
        public string JwtToken { get; set; }

        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }
    }
}
