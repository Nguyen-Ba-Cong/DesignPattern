using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Database.Entity;
using DesignPattern.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace DesignPattern.Service.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly DesignPatternDBContext _baseContext;
        public BaseRepository(DesignPatternDBContext dbContext)
        {
            _baseContext = dbContext;
        }
        public IQueryable<T> All(int offset, int limit)
        {
            return _baseContext.Set<T>().Skip(offset * limit).Take(limit).AsNoTracking();
        }

        public T Create(T entity)
        {
            _baseContext.Set<T>().Add(entity);
            _baseContext.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _baseContext.Set<T>().Remove(entity);
            _baseContext.SaveChanges();
            return entity;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _baseContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T Update(T entity)
        {
            _baseContext.Set<T>().Update(entity);
            _baseContext.SaveChanges();
            return entity;
        }
    }
}
