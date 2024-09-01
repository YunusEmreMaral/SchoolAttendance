using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance_DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIDAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}
