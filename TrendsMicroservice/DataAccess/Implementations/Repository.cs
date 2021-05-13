using DataAccess.Abstractions;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        ICollection<T> IRepository<T>.GetAll(params string[] includes)
        {
            IQueryable<T> query = entities;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }

        T IRepository<T>.GetById(Guid guid, params string[] includes)
        {
            IQueryable<T> query = entities.Where(entity => entity.Id == guid);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.SingleOrDefault();
        }

        T IRepository<T>.GetByFilter(Expression<Func<T, bool>> filter)
        {
            return entities.SingleOrDefault(filter);
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

        ICollection<T> IRepository<T>.GetAllByFilter(Expression<Func<T, bool>> filter, params string[] includes)
        {
            IQueryable<T> query = entities.Where(filter);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }
    }
}
