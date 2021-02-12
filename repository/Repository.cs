using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace nexo.repository
{
    public interface IRepository<T> where T: class
    {
        T GetById (string id);

        List<T> GetAll();

        void Update(T entity);

       void Delete(T entity);

       void insert(T entity);

       List<T> Query(Expression<Func<T, bool>> filter);

    }
}