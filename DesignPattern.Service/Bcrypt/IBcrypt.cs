using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Bcrypt
{
    public interface IBcrypt
    {
        public string HashCode(string password);
    }
}
