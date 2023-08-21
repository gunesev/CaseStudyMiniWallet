using CaseStudy.Data.DataContract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CaseStudy.Data
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DbSet<T> _objectSet;
        protected DbContext _context;
        private readonly IRepository<T> _repository;
        public Repository(DbContext context)
        {
            _context = context;
            _objectSet = _context.Set<T>();
        }

        public bool Insert(T obj)
        {
            _objectSet.Add(obj);
            bool status = Save();
            _context.Entry(obj).State = EntityState.Detached;
            return status;
        }

        public bool Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            bool status = Save();
            _context.Entry(obj).State = EntityState.Detached;
            return status;
        }

        public bool Delete(T obj)
        {
            _objectSet.Remove(obj);
            bool status = Save();
            _context.Entry(obj).State = EntityState.Detached;
            return status;
        }

        public bool DeleteRange(T[] obj)
        {
            _objectSet.RemoveRange(obj);
            bool status = Save();
            _context.Entry(obj).State = EntityState.Detached;
            return status;
        }

        private bool Save()
        {
            bool transactionResult = _context.SaveChanges() > 0;
            return transactionResult;
        }

        public IQueryable<T> List(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _objectSet;
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query);
            var res = query.AsNoTracking();
            return res;
        }

        public IEnumerable<T> ListAsEnumerable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IEnumerable<T> query = _objectSet;
            if (filter != null)
                query = query.AsEnumerable().Where(filter.Compile());

            if (orderBy != null)
                return orderBy(query.AsQueryable());

            return query;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where);
        }

        public IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).AsNoTracking();
        }
    }
}