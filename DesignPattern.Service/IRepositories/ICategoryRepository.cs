using DesignPattern.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public List<New> GetNewByCategoryId(int id);
    }
}
