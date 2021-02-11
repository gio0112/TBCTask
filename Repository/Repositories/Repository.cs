using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetByID(object id)
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
        public void Delete(object id)
        {
            T entity = entities.Find(id);
            Delete(entity);
        }
    }
}
