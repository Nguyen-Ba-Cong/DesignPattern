using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Email
{
    public interface ISendMail
    {
        public Task<bool> SendMailMethod(string email, string content, string subject);
    }
}
