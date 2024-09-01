using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return _context.Set<T>().ToList();
        }
        public List<T> GetListByFilter(Expression<Func<T, bool>> filter) // buna bi bak tekrardan cok onemli metod 
        {
            
            return _context.Set<T>().Where(filter).ToList();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
