using DesignPattern.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.IRepositories
{
    public interface INewRepository : IBaseRepository<New>
    {
        public New AddNew(string email, New neww);
        public New DeleteNew(string email, New neww);
        public New UpdateNew(string email, New neww);
    }
}
