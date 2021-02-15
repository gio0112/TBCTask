using Domain.Common;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }
        public T GetByID(int id)
        {
            return entities.Find(id);
        }
        public void Insert(T entity)
        {
            entities.Add(entity);
        }
        public void Update(T entity)
        {
            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            T entity = entities.Find(id);
            context.Remove(entity);
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.AsQueryable();
        }
    }
}
