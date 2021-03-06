﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVideoAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(object id);
        Task<T> GetFirst(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                 string includeProperties = "",
                 int first = 0, int offset = 0);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
