using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task TAddAsync(T t);
        Task TDeleteAsync(T t);
        Task TUpdateAsync(T t);
        Task<T> TGetByIDAsync(int id);
        Task<List<T>> TGetListAsync();
        Task<List<T>> TGetListByFilterAsync(Expression<Func<T, bool>> filter);
    }

}
