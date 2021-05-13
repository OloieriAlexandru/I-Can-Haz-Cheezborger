using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;

namespace DataAccess.Abstractions
{
    // https://chathuranga94.medium.com/generic-repository-pattern-for-asp-net-core-9e3e230e20cb
    public interface IRepository<T> where T : BaseEntity
    {
        ICollection<T> GetAll(params string[] includes);

        ICollection<T> GetAllByFilter(Expression<Func<T, bool>> filter, params string[] includes);

        T GetById(Guid guid, params string[] includes);

        T GetByFilter(Expression<Func<T, bool>> filter);

        void Insert(T entity);
        
        void Update(T entity);
        
        void Delete(Guid guid);
        
        void SaveChanges();
    }
}
