using System.Linq.Expressions;

namespace CaseStudy.Data.DataContract
{
    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> List(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        IQueryable<T> Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> where);

        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(T obj);
    }
}
