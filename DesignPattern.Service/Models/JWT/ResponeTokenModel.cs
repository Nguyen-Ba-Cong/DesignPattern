using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Models
{
    public class ResponeTokenModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Scope { get; set; }
    }
}
