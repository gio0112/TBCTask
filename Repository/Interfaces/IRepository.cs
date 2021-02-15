using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetByID(int id);
        IQueryable<T> Search(Expression<Func<T, bool>> filter = null);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
