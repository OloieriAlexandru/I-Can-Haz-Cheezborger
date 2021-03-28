using DataAccess.Abstractions;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    // https://chathuranga94.medium.com/generic-repository-pattern-for-asp-net-core-9e3e230e20cb
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> entities;

        public Repository(AppDbContext _context)
        {
            context = _context;
            entities = context.Set<T>();
        }

        ICollection<T> IRepository<T>.GetAll()
        {
            return entities.ToList();
        }

        T IRepository<T>.GetById(Guid guid)
        {
            return entities.SingleOrDefault(entity => entity.Id == guid);
        }

        void IRepository<T>.Insert(T entity)
        {
            entities.Add(entity);
        }

        void IRepository<T>.Update(T entity)
        {
            entities.Update(entity);
        }

        void IRepository<T>.Delete(Guid guid)
        {
            T entity = entities.SingleOrDefault(entity => entity.Id == guid);
            entities.Remove(entity);
        }

        void IRepository<T>.SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
