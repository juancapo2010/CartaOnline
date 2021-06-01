using CartaOnline.Config;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CartaOnline.Repositories
{
    public interface IRepositoryGeneric
    {
        void Add<T>(T entity) where T : class;
        List<T> Traer<T>() where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteBy<T>(int id) where T : class;
        T FindBy<T>(int id) where T : class;
        
    }
    public class RepositoryGeneric : IRepositoryGeneric
    {
        private readonly AppDbContext _context;

        public RepositoryGeneric(AppDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteBy<T>(int id) where T : class
        {
            
            T entity = FindBy<T>(id);
            if(entity != null)
                Delete<T>(entity);
        }

        public T FindBy<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Traer<T>() where T : class
        {
            List<T> query = _context.Set<T>().ToList();
            return query;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
