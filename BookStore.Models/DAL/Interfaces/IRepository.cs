﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<List<T>> GetAll();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task Delete(Expression<Func<T, bool>> expression);
        void Update(T entity);
        IQueryable<T> GetAllPaging();
        void Delete(T entity);
    }
}
