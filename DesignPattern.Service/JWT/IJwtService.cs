using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.JWT
{
    public interface IJwtService
    {
        public bool ValidateCurrentToken(string token);
        public string RandomTokenString();
        public string GenerateJWT(string email, string role);
    }
}
