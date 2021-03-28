using System;
using System.Collections.Generic;

using Entities;

namespace DataAccess.Abstractions
{
    // https://chathuranga94.medium.com/generic-repository-pattern-for-asp-net-core-9e3e230e20cb
    public interface IRepository<T> where T : BaseEntity
    {
        ICollection<T> GetAll();

        T GetById(Guid guid);
        
        void Insert(T entity);
        
        void Update(T entity);
        
        void Delete(Guid guid);
        
        void SaveChanges();
    }
}
